using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    
    void Update()
    {
        transform.localEulerAngles = -transform.parent.localEulerAngles;
    }
}
