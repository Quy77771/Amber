using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAmmo : Ammo
{
   private float baseAmmoDmg = 4f;
   private float baseAmmoSpeed = 20f;
   [SerializeField] GameObject baseAmmoImpact;
   protected override void Awake()
   {
      AmmoDmg = baseAmmoDmg;
      AmmoSpeed = baseAmmoSpeed;
      Impact = baseAmmoImpact;
      base.Awake();
   }
}
