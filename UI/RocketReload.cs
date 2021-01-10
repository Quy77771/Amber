using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketReload : MonoBehaviour
{
    [SerializeField] private Image fillGunBar;
    [SerializeField] private Slider gunReloadSlide;

    [Tooltip("Time wait for reload")]
    [SerializeField] private  float timeReload = 2f;

    private void Awake() {
        this.enabled = false;
    }
    void Start()
    {
        fillGunBar.color = new Color(250f,185f,0f);
        GameManager.Instance.OnShoot += Reload;
        gunReloadSlide.value = 1f;
        
    }

    public void Reload(){
            GameManager.Instance.ChangedGunState();
            StartCoroutine(OnReload());
    }
    private IEnumerator OnReload(){
        while(gunReloadSlide.value > 0){
            gunReloadSlide.value -= Time.deltaTime / timeReload;
            yield return null;
        }
        GameManager.Instance.ChangedGunState();
        gunReloadSlide.value = 1f;
    }

}
