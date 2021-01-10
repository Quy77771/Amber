using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : HitPlayerByBody
{
    public override void SetDamage()
    {
        damage = GameSetting.SPIKE_DAMAGE;
    }
}
