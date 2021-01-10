using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
   // public GunState gunState;
    // [HideInInspector] public GameObject ammo;
    [SerializeField] private Joystick dirMove;
    [SerializeField] private Transform transformAmmo;
    [HideInInspector] public Vector2 lastDir;
    public float hp = 10f, maxHp = 10f;
    private Rigidbody2D rgPlayer;
    public float speed;
 

    private void Awake() {
        if(Instance == null)
            Instance = this;
    }
    void Start()
    {
     
       // GameManager.Instance.ChangedGunState += OnGunChangedState;
        
        GameManager.Instance.OnHpChanged += OnHpChanged;
        rgPlayer = GetComponent<Rigidbody2D>();
        lastDir = dirMove.Input;
    }

    void Update()
    {
        rgPlayer.velocity = dirMove.Input * speed;
        transform.up = new Vector3(lastDir.x, lastDir.y, 0);
        if(GameManager.Instance.ShootBy == GunState.ShootByLaserAndRocket){
            if(Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.GetGunState() == GunState.Shooting)
                GameManager.Instance.OnShoot();
        }
        else if(GameManager.Instance.ShootBy == GunState.ShootByLineLaser){
            if(Input.GetKey(KeyCode.Space))
                GameManager.Instance.OnLineLaserShoot();
            else if(Input.GetKeyUp(KeyCode.Space))
                GameManager.Instance.ShootOff();
        }
            
    } 

    void LateUpdate()
    {
        if(dirMove.Input != Vector2.zero)
            lastDir = dirMove.Input;
        else{
            rgPlayer.velocity = Vector3.zero;
            rgPlayer.angularVelocity = 0f;
        }
    }

    
    // private void  SpawnAmmo() 
    // {
    //     SimplePool.Spawn(ammo, transformAmmo.position, transform.rotation);
    // }

    public void OnHpChanged(float dmg){
        hp += dmg;
        if(hp <= 0)
            Destroy(gameObject);
    }

    private void OnDisable() {
        GameManager.Instance.OnHpChanged -= OnHpChanged;
    }

    // private void OnGunChangedState(){
    //     if(gunState == GunState.Shooting)
    //         gunState = GunState.Reload;
    //     else
    //         gunState = GunState.Shooting;
    // }

}
// public enum GunState{
//     Shooting, Reload
// }