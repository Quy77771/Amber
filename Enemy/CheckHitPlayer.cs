using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHitPlayer : HitPlayerByBody
{
    public override void SetDamage()
    {
        damage = GetComponentInParent<Enemy>().AtkDmg;
    }
}
