using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIndicator : MonoBehaviour
{
    // // Start is called before the first frame update
    // private Color insideColor, outsideColor;
    // private bool currentIsInSide = false;
    // [HideInInspector]
    // public bool lateIsInSide = false;
    // private float radius;
    

    // private void OnEnable() {
        
    //     insideColor = Color.white;
    //     outsideColor = new Color32(255,85,85,255);
    //     // outsideColor = Color.red;
    //     radius = GetComponent<Renderer>().bounds.size.x / 2;
    // }

    
    // void Update()
    // {
    //     if(SqrDistanceToCastSkill() <= radius * radius)
    //         currentIsInSide = true;
    //     else
    //         currentIsInSide = false;
        
    //     if(currentIsInSide != lateIsInSide){
    //         if(currentIsInSide){
    //             GetComponent<SpriteRenderer>().color = insideColor;
    //             Debug.Log("in");
    //         }
    //         else{
    //             GetComponent<SpriteRenderer>().color = outsideColor;
    //             Debug.Log("out");
    //         }
    //     }

    //     lateIsInSide = currentIsInSide;      
    // }

    // private float SqrDistanceToCastSkill(){
    //     return ((Vector2)(transform.position - GameManager.Instance.posCastSkill())).sqrMagnitude;
    // }
}
