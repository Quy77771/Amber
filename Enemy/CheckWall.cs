using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour
{
    [HideInInspector] public bool canMoveTo;
    private void OnTriggerEnter2D(Collider2D other) {
        canMoveTo = false;
    }

}
