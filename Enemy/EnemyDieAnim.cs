using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieAnim : MonoBehaviour
{
    public void Despawn(){
        SimplePool.Despawn(gameObject);
    }
}
