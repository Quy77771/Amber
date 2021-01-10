using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmo : Ammo
{
   private float baseAmmoDmg = 1f;
   private float baseAmmoSpeed = 20f;
   [SerializeField] GameObject baseAmmoImpact;
   protected override void Awake()
   {
      AmmoDmg = baseAmmoDmg;
      AmmoSpeed = baseAmmoSpeed;
      Impact = baseAmmoImpact;
      base.Awake();
   }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
         if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().TakeDamage(AmmoDmg);
        }
    }
}

