// using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Vector3 = UnityEngine.Vector3;

public class AlienSpider : Enemy, IStun
{
    [Header("Detail and attribute of enemy")]

    [Tooltip("Khi nhận sát thương thì chuyển sang material này")]
    [SerializeField] private Material whiteFlash;

    [Range(5,10)][SerializeField] private float hpAlienSpider = 5f;
    [Range(1.5f,3)][SerializeField] private float atkAlienSpider = 2f;
    [Range(2,4)][SerializeField] private float speedAlienSpider = 2.5f;
    [Tooltip("Hiệu ứng khi enemy chết")]
    [SerializeField] private GameObject dieEffectSpider;
    private Animator anim;
    [Tooltip("Cảnh báo chuẩn bị tung đòn tấn công")]
    [SerializeField] private GameObject warning;

    private EnemyState state;
    [Header("Area of idle state")]
    #region  biến trạng thái idle
    [Tooltip("Vùng di chuyển ngẫu nhiên khi đang trong trạng thái idle")]
    [SerializeField] private Vector2 offSet;
    private Vector2 posIdle;
    protected float rangeChase;
    [SerializeField] private GameObject checkWall;
    #endregion
    private Vector3 dirFromThisToPlayer;
    
    protected override void OnEnable()
    {
        posIdle = transform.position;
        
        warning.SetActive(false);
        anim = GetComponent<Animator>();

        Hp = hpAlienSpider;
        AtkDmg = atkAlienSpider;
        DieEffect = dieEffectSpider;
        SpWhiteFlash = whiteFlash;
        Speed = speedAlienSpider;
        base.OnEnable();

        state = EnemyState.MoveAround;
    }
    private void FixedUpdate() {
        // if(state == EnemyState.MoveAround)
        //     Idle();

        // if(DistanceFromThisToPlayer().sqrMagnitude <= GameSetting.AS_ATTACK_RANGE * GameSetting.AS_ATTACK_RANGE && state == EnemyState.ChaseAndCanAtk)
        //     CheckAttack();
        if(DistanceFromThisToPlayer().sqrMagnitude > GameSetting.AS_CHASE_RANGER * GameSetting.AS_CHASE_RANGER)
            Idle();
        else
            CheckAttack();
    }

    #region  xử lý Idle

    // mỗi lần chuyển hàm phải kiểm tra trạng thái trước đó.
    // vì có thể quá trình sẽ bị dán đoạn ( đang nghỉ gặp player thì dừng nghỉ để đuổi)
    // ngoài ra trong quá trình thực hiện Coroutine cũng phải check trạng thái. Vì có thể bị dán đoạn
    // mà Coroutine ko được hoàn thành
    private void Idle(){
        if(state == EnemyState.MoveAround){
            Debug.Log(1);
            state = EnemyState.Idle;
            StartCoroutine(OnIdle(GameSetting.AS_IDLE_TIME));
        }
    }

    // trạng thái nghỉ đc thể hiện là tắt finding và để đứng yên
    private IEnumerator OnIdle(float time){
        anim.Play("Idle");
        GetComponent<AIPath>().enabled = false;
        while(time > 0 && state == EnemyState.Idle){
            time -= Time.fixedDeltaTime;
            yield return null;
        }
        MoveAround();
    }

    // enemy đầu tiên sẽ chọn 1 vị trí ngẫu nhiên trong offSet. sau đó thả vào đó 1 go để check trigger với wall
    // (layer đã được set chỉ có thể trigger với tường)
    // nếu phản hồi lại vị trì đó là wall, sẽ thực hiện lại cho đến khi tìm đc điểm đến phù hợp
    private void MoveAround(){
        if(state == EnemyState.Idle){
            state = EnemyState.MoveAround;

            float x,y;
            Vector2 randomDir;
            bool canMoveTo;

            do{
                x = Random.Range(-offSet.x, offSet.x);
                y = Random.Range(-offSet.y, offSet.y);
                randomDir = new Vector2(x,y);

                checkWall.transform.position = randomDir + posIdle;
                canMoveTo = checkWall.GetComponent<CheckWall>().canMoveTo;

            }while(!canMoveTo);

            GetComponent<AIPath>().enabled = true;
            GetComponent<AIDestinationSetter>().target = checkWall.transform;
            StartCoroutine(OnMoveAround(checkWall.transform.position));
        }
    }
    WaitForFixedUpdate waitForNextFixedUpdate = new WaitForFixedUpdate();
    // sau khi chạy đến nơi thì về lại vòng lặp Idle
    private IEnumerator OnMoveAround(Vector3 pos){
        anim.Play("MoveAnim");
        while((pos - transform.position).sqrMagnitude > 0.5 && state == EnemyState.MoveAround){
            yield return waitForNextFixedUpdate;
        }
        Debug.Log(2);
        Idle();
    }
    #endregion

    #region  xử lý tấn công
    private void CheckAttack(){
        // chỉ check va chạm với tường. nếu ko có bất kỳ vật cản nào phía trước tiến vào state PreAttack
        int layerMark = 1 << 9;
        Vector3 dir = DistanceFromThisToPlayer();
        if(Physics2D.Raycast(transform.position, dir, dir.magnitude, layerMark).collider == null){
            
            state = EnemyState.OnAttack;
            PreAttack(dir);
        }
        // nếu có vật cản thì tiếp tục tìm kiếm player
        else
            state = EnemyState.ChaseAndCanAtk;
    }

    private void PreAttack(Vector3 dir){
        state = EnemyState.PreAttack;
        dirFromThisToPlayer = dir;
        anim.Play("AttackingAnim");
        GetComponent<AIPath>().enabled = false;
        warning.SetActive(true);
        warning.transform.up = Vector2.up;
        // tiếp theo chuyển đến hàm OnAttack được gắn trên thanh anim
    }

    // khoảng thời gian chờ giữa PreAttack và OnAttack là 1s. Thể hiện trên thanh AttackingAnim

    /// <summary>
    ///  hàm này được gắn trên thanh AttackingAnim
    /// </summary>
    public void OnAttack(){
        warning.SetActive(false);
        state = EnemyState.OnAttack;
        transform.up = dirFromThisToPlayer;
        // thời gian bắt đầu nhảy đến vị trí player là 0.5s, thể hiện trên thanh AttackingAnim
        StartCoroutine(Attacking(GameSetting.AS_JUMP_ATTACK_TIME, dirFromThisToPlayer));
    }

    private IEnumerator Attacking(float time, Vector3 dirFromThisToPlayer){
        float _time = time;
        while(time > 0){
            // transform.Translate(dirFromThisToPlayer/_time * Time.deltaTime);
            transform.position += dirFromThisToPlayer/_time * Time.fixedDeltaTime;
            time -= Time.fixedDeltaTime;
            yield return null;
        }
        AfterAttack();
    }

    private void AfterAttack(){
        state = EnemyState.ChaseAndCantAtk;
        anim.Play("MoveAnim");
        GetComponent<AIPath>().enabled = true;
        StartCoroutine(WaitForNextAttack(GameSetting.AS_WAIT_FOR_NEXT_ATTACK_TIME));
    }
    private IEnumerator WaitForNextAttack(float time){
        yield return new WaitForSeconds(time);
        state = EnemyState.ChaseAndCanAtk;
    }
    #endregion


    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        Hp -= dmg;
    }

    #region xử lý Stun
    public void OnStun( float time)
    {
        StartCoroutine(Stunning(time, state));
    }

    private IEnumerator Stunning(float time, EnemyState lastState){
        state = EnemyState.OnStun;

        // thể hiện của trạng thái stun là tắt anim và di chuyển của pathFinding
        GetComponent<AIPath>().enabled = false;
        GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<AIPath>().enabled = true;
        GetComponent<Animator>().enabled = true;

        //  sau khi bị stun. nếu đang ở trạng thái tìm kiếm player thì tiếp tục tìm
        if(lastState == EnemyState.ChaseAndCanAtk)
            state = EnemyState.ChaseAndCanAtk;

        // nếu ko ( tức đang trong trạng thái tấn công bị ngắt quãng) thì mất lượt đánh và đợi reset
        else{
            state = EnemyState.ChaseAndCantAtk;
            StartCoroutine(WaitForNextAttack(GameSetting.AS_WAIT_FOR_NEXT_ATTACK_TIME));
        }
    }
    #endregion

}
