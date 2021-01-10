using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ThunderBolt : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject areaEffect;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject thunderBolt;
    [SerializeField] private Image coolDown;
    
    private GameObject myAreaEffect;
    private Transform tfMyAreaEffect;
    private UseSkill state;

    // hướng từ eventData đến areaEffect
    private Vector2 dir;

    #region  skill indicator
    [SerializeField] private GameObject skillIndicator;
    private GameObject mySkillIndicator;
    [SerializeField] private GameObject player;
     private Color insideColor, outsideColor;
    private bool currentIsInSide,  lateIsInSide;
    private float radius;
    private float SqrDistanceToCastSkill(){
        return ((Vector2)(mySkillIndicator.transform.position - PosCastSkill())).sqrMagnitude;
    }
    
    #endregion

    private void Start() {
        state = UseSkill.ChooseSpell;
       
        coolDown.fillAmount = 0;
        insideColor = Color.white;
        outsideColor = new Color32(255,85,85,255);
        mySkillIndicator = Instantiate(skillIndicator, player.transform);
       
        radius = mySkillIndicator.GetComponent<Renderer>().bounds.size.x / 2;
        mySkillIndicator.SetActive(false);
   }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if(state == UseSkill.ChooseSpell){

            // sinh ra vùng effect. gắn làm con của thunderbolt icon
            myAreaEffect =  SimplePool.Spawn(areaEffect,GetComponent<RectTransform>().position, Quaternion.identity);
            myAreaEffect.transform.up = dir;
            tfMyAreaEffect = myAreaEffect.transform;
            myAreaEffect.transform.SetParent(gameObject.transform);

            // tính khoảng cách để đổi màu
            if(SqrDistanceToCastSkill() <= radius * radius){
                mySkillIndicator.GetComponent<SpriteRenderer>().color = insideColor;
                currentIsInSide = true;
            }
            else{
                mySkillIndicator.GetComponent<SpriteRenderer>().color = outsideColor;
                currentIsInSide = false;
            }

            mySkillIndicator.SetActive(true);
            
            dir = ( - PosCastSkill() + PlayerControl.Instance.transform.position).normalized;

            // đổi state. set max cooldown
            state = UseSkill.ChooseTarget;
            coolDown.fillAmount = 1f;
            
        }    
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        
        if(state == UseSkill.ChooseTarget){
            dir = (- PosCastSkill() + PlayerControl.Instance.transform.position).normalized;
            Vector2 posCastSkill = eventData.position;
            tfMyAreaEffect.position = posCastSkill;
            tfMyAreaEffect.transform.up = dir;
        }


        // xử lý skill indicator
        if(SqrDistanceToCastSkill() <= radius * radius)
            currentIsInSide = true;
        else
            currentIsInSide = false;
        
        if(currentIsInSide != lateIsInSide){
            if(currentIsInSide){
                mySkillIndicator.GetComponent<SpriteRenderer>().color = insideColor;
            }
            else{
                mySkillIndicator.GetComponent<SpriteRenderer>().color = outsideColor;
            }
        }
        lateIsInSide = currentIsInSide; 
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if(state == UseSkill.ChooseTarget){
            // nếu thả bên trong thì cast skill
            if(lateIsInSide){
                CastThunderBolt();
                state = UseSkill.Casting;
            }

            // ko thì reset
            else{
                SimplePool.Despawn(myAreaEffect);
                state = UseSkill.ChooseSpell;
                coolDown.fillAmount = 0f;
            }
        }

        mySkillIndicator.SetActive(false);
        
    }

    // vị trí areaEffect ứng với wolrd
    private Vector3 PosCastSkill(){
        if(tfMyAreaEffect != null){
            Vector3 pos = camera.ScreenToWorldPoint(tfMyAreaEffect.position);
            return new Vector3(pos.x, pos.y, 0);
        }
        return transform.position;
    }

    private void CastThunderBolt(){
        coolDown.fillAmount = 1;
        GameObject myThunderBolt = SimplePool.Spawn(thunderBolt, PosCastSkill(), tfMyAreaEffect.rotation);
        SimplePool.Despawn(myAreaEffect);
        StartCoroutine(Casting(myThunderBolt));
        StartCoroutine(CoolDown());
        state = UseSkill.CoolDown;
    }

    private IEnumerator Casting(GameObject mySpell){
        yield return new WaitForSeconds(GameSetting.THUNDER_BOLT_EFFECT_TIME);
        SimplePool.Despawn(mySpell);
    }


    private IEnumerator CoolDown(){
        while(coolDown.fillAmount > 0){
            coolDown.fillAmount -= 1 / GameSetting.THUNDER_BOLT_COOLDOWN_TIME * Time.deltaTime;
            yield return null;
        }
        state = UseSkill.ChooseSpell;
    }
 
}
