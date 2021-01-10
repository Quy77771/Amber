using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour
{
    private float hp ;
    private float speed;
    private SpriteRenderer spriteRenderer;
    private float atkDmg;
    private Material spWhiteFlash, spDefault;
    private GameObject dieEffect;
    public float Hp {  get => hp; protected set => hp = value; }
    public float AtkDmg { get => atkDmg; set => atkDmg = value; }
    protected GameObject DieEffect { get => dieEffect; set => dieEffect = value; }
    protected Material SpWhiteFlash { get => spWhiteFlash; set => spWhiteFlash = value; }
    public float Speed { get => speed; set => speed = value; }


    protected virtual void OnEnable()
    {   
        GetComponent<AIPath>().maxSpeed = speed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spDefault = spriteRenderer.material;
    }
  
    protected void Die(){
        if(hp <= 0){
            SimplePool.Spawn(dieEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator TakeDamageEffect(){
        spriteRenderer.material = spWhiteFlash;
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRenderer.material = spDefault;
    }
    public virtual void TakeDamage(float dmg){
        StartCoroutine(TakeDamageEffect());
        Die();
    }

    protected Vector3 DistanceFromThisToPlayer(){
        if(PlayerControl.Instance != null)
            return ( - transform.position + PlayerControl.Instance.transform.position);
        return transform.position;
    }

}
