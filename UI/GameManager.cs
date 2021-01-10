using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   // private Dictionary<EventID, List<ObseverMember>> ListMember = new Dictionary<EventID, List<ObseverMember>>();
    public static GameManager Instance;

    public Action OnShoot;
    public Action ChangedGunState;
    public Action<float> OnHpChanged;
    public Func<GunState> GetGunState;
    public Action OnLineLaserShoot;
    public Action ShootOff;
    public GunState ShootBy;

    public Func<Vector3> posCastSkill;
   
    
    public Camera mainCam;


    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    
  

}

