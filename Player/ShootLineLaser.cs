using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLineLaser : MonoBehaviour
{
    private Transform firePoint;
    [SerializeField] private float distance = 20f;
    [SerializeField] private LineRenderer lineShoot;
    List<RaycastHit2D> results = new List<RaycastHit2D>();

    // phép dịch bit.
    private int layerMark = 1 << 9 + 1 << 13 ;
    private ContactFilter2D contact;
    void OnEnable()
    {
        GameManager.Instance.OnLineLaserShoot += Shoot;
        GameManager.Instance.ShootOff += ShootOff;
        contact.layerMask.value = layerMark;
    }

    private void Shoot(){
        transform.up = PlayerControl.Instance.transform.up;
        Physics2D.Raycast(transform.position, transform.up, contact, results, distance);
        
        foreach( RaycastHit2D item in results){
            if(item.collider.tag == "Enemy")
                item.collider.gameObject.GetComponent<Enemy>().TakeDamage(GameSetting.LINE_LASER_DAMAGE_PER_SECOND * Time.deltaTime);
            else if(item.collider.tag == "Wall")
                Draw2DRay(transform.position, item.point);
            else
                Draw2DRay(transform.position, transform.up * distance);
        }
    }

    private void ShootOff(){
        lineShoot.enabled = false;
    }
    private void Draw2DRay(Vector2 start, Vector2 end){
        lineShoot.enabled = true;
        lineShoot.SetPosition(0,start);
        lineShoot.SetPosition(1,end);
    }
}
