using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBoltDamage : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
       other.GetComponent<IStun>().OnStun(GameSetting.THUNDER_BOLT_EFFECT_TIME);
   }

private List<Collider2D> collider2Ds = new List<Collider2D>();

private float countTime = GameSetting.THUNDER_BOLT_DEAL_DAMAGE_EACH_TIME;
   private void OnTriggerStay2D(Collider2D other) {
       if(!collider2Ds.Contains(other))
            collider2Ds.Add(other);
   }


// vì OnTriggerStay2D ko thể phát hiện va chạm với đồng thời nhiều go nên phải xử lý như này
// dùng lateUpdate để đảm bảo chạy sau khi add vào list
private void LateUpdate() {
    if(countTime >= GameSetting.THUNDER_BOLT_DEAL_DAMAGE_EACH_TIME){
        foreach(Collider2D other in collider2Ds)
                // kiểm tra enemy còn sống ko? chết rồi thì ko lấy component đc. lỗi
                if(other != null)
                    other.GetComponent<Enemy>().TakeDamage(GameSetting.THUNDER_BOLT_DAMAGE);
        countTime = 0;
    }

    countTime += Time.deltaTime;
   }
}
