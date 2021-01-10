using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ChooseWeapon : MonoBehaviour
{
    [SerializeField] private Button laser, rocket, lineLaser;
    [SerializeField] SpriteRenderer weapon;
    [SerializeField] GameObject chooseWeaponCanvas , ReloadGunBar;

    void Start()
    {
        ReloadGunBar.SetActive(false);
        laser.onClick.AddListener(ChooseLaser);
        rocket.onClick.AddListener(ChooseRocket);
        lineLaser.onClick.AddListener(ChooseLineLaser);
        Time.timeScale = 0;
    }

    private void ChooseLaser(){
        weapon.sprite = Resources.Load<Sprite>("Laser");
        weapon.gameObject.AddComponent<ShootLaserAndRocket>();
        weapon.GetComponent<ShootLaserAndRocket>().SetAmmo(Resources.Load<GameObject>("LaserAmmo"));
        
        GameManager.Instance.ShootBy = GunState.ShootByLaserAndRocket;
        ReloadGunBar.GetComponent<LaserReload>().enabled = true;
        
        chooseWeaponCanvas.SetActive(false);
        ReloadGunBar.SetActive(true);
        Time.timeScale = 1;
    }

    private void ChooseRocket(){
        weapon.sprite = Resources.Load<Sprite>("Rocket");
        weapon.gameObject.AddComponent<ShootLaserAndRocket>();
        weapon.GetComponent<ShootLaserAndRocket>().SetAmmo(Resources.Load<GameObject>("RocketAmmo"));
        GameManager.Instance.ShootBy = GunState.ShootByLaserAndRocket;
        ReloadGunBar.GetComponent<RocketReload>().enabled = true;
       
        chooseWeaponCanvas.SetActive(false);
        ReloadGunBar.SetActive(true);
        Time.timeScale = 1;
    }

    private void ChooseLineLaser(){
        weapon.sprite = Resources.Load<Sprite>("LineLaser");
       // GameManager.Instance.SetAmmo(Resources.Load<GameObject>("LineLaserAmmo"));
        weapon.gameObject.GetComponent<ShootLineLaser>().enabled = true;
        // weapon.gameObject.GetComponent<ShootLaserAndRocket>().enabled = false;
         GameManager.Instance.ShootBy = GunState.ShootByLineLaser;
        chooseWeaponCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
