using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocKetImpactAnim : ImpactAmmoAnim
{
    private void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.GetComponent<Enemy>().TakeDamage(GameSetting.ROCKET_DAMAGE);
    }
}
