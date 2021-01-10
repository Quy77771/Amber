using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserReload : MonoBehaviour
{
   
    private Gradient gunBarColor;
    [SerializeField] private Image fillGunBar;
    [SerializeField] private Slider gunReloadSlide;

    #region  speedReload
    [Header("Speed atribute of Reload")]

    [Tooltip("Speed gain ReloadBar when player shooting")]
    [SerializeField] private float speedGainReload = 0.15f;

    [Tooltip("Speed reduce ReloadBar when player cease shoot")]
    [SerializeField] private float speedReduceReload = 0.1f;

    [Tooltip("Time wait for reload")]
    [SerializeField] private  float timeReload = 4f;
    #endregion

  private void Awake() {
      this.enabled = false;
  }
    void Start()
    {
        GameManager.Instance.OnShoot += Reload;
        gunBarColor = CreateGradient();
        
        gunReloadSlide.value = 0;
        
    }

    private void Update() {
        fillGunBar.color = gunBarColor.Evaluate(gunReloadSlide.value);
        if(GameManager.Instance.GetGunState() == GunState.Shooting)
            gunReloadSlide.value -= speedReduceReload * Time.deltaTime;
    }

    public void Reload(){
        gunReloadSlide.value += speedGainReload;
        if(gunReloadSlide.value >= 1){
            GameManager.Instance.ChangedGunState();
            StartCoroutine(OnReload());
        }
    }
    private IEnumerator OnReload(){
        while(gunReloadSlide.value >0){
            gunReloadSlide.value -= Time.deltaTime / timeReload;
            yield return null;
        }
        GameManager.Instance.ChangedGunState();

    }

    private Gradient CreateGradient(){
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = new Color(250f, 185f, 0f);
        colorKey[0].time = 0.706f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        gradient.mode = GradientMode.Fixed;
        return gradient;
    }
}

