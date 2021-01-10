using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ammo : MonoBehaviour
{
    public float AmmoDmg { get => ammoDmg; protected set => ammoDmg = value; }
    protected float AmmoSpeed { get => ammoSpeed; set => ammoSpeed = value; }
    protected GameObject Impact { get => impact; set => impact = value; }

    private Rigidbody2D rgAmmo;
    private GameObject impact;
    private float ammoDmg;
    private float ammoSpeed;

    protected virtual  void Awake()
    {
        rgAmmo = GetComponent<Rigidbody2D>();
    }
    protected virtual void OnEnable()
    {
        rgAmmo.velocity = transform.up * ammoSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        SimplePool.Spawn(impact, gameObject.transform.position, Quaternion.identity);
        SimplePool.Despawn(gameObject);
        // if(other.gameObject.tag == "Enemy"){
        //     other.gameObject.GetComponent<Enemy>().TakeDamage(ammoDmg);
        // }
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     SimplePool.Spawn(impact, gameObject.transform.position, Quaternion.identity);
    //     SimplePool.Despawn(gameObject);
    //     if(other.gameObject.tag == "Enemy"){
    //         other.gameObject.GetComponent<Enemy>().TakeDamage(ammoDmg);
    //     }
    // }
}


