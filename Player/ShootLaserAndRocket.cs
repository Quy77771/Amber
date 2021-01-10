using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserAndRocket : MonoBehaviour
{
   private GunState gunState;
   private GameObject ammo;
    void Start()
    {
        gunState = GunState.Shooting;
        GameManager.Instance.OnShoot += SpawnAmmo;
        GameManager.Instance.ChangedGunState += OnGunChangedState;
        GameManager.Instance.GetGunState += GetGunState;
       
    }

    private void  SpawnAmmo() 
    {
        SimplePool.Spawn(ammo, transform.position, transform.rotation);
    }

    private void OnGunChangedState(){
        if(gunState == GunState.Shooting)
            gunState = GunState.Reload;
        else
            gunState = GunState.Shooting;
    }

    private GunState GetGunState(){
        return gunState;
    }


    public void SetAmmo(GameObject ammo){
        this.ammo = ammo;
    }

    // private void OnDisable() {
    //     GameManager.Instance.OnShoot -= SpawnAmmo;
    //     GameManager.Instance.ChangedGunState -= OnGunChangedState;
    //     GameManager.Instance.SetAmmo -= SetAmmo;
    // }
}

