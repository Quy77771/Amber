using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitPlayerByBody : MonoBehaviour
{
    protected float damage;
    private float countTime = GameSetting.HIT_PLAYER_TIME;

    public abstract void SetDamage();
    private void Start() {
        SetDamage();
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(countTime >= GameSetting.HIT_PLAYER_TIME){
            GameManager.Instance.OnHpChanged(-damage);
            countTime = 0f;
        }
        countTime += Time.deltaTime;

    }
}
