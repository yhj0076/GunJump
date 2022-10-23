using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeLegAttack : MonoBehaviour
{
    public GameObject firePoint;

    private LineRenderer bulletLine;
    PlayerController playerController;
    ThreeLegController threeLegController;
    public bool isFire = false;         //발사 명령

    public int damage;
    public float BackOff;

    private int FrontCheck = 0;

    void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        threeLegController = GetComponent<ThreeLegController>();

        bulletLine.positionCount = 2;
        bulletLine.enabled = false;
    }

    void Blast()
    {
        RaycastHit2D hit;

        FrontCheck = threeLegController.frontCheck * -1;

        //총구 위치
        Vector2 ShotStart = new Vector2(firePoint.transform.position.x,
                                        firePoint.transform.position.y);
        Vector2 hitPosition = Vector2.zero;
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("ThreeLeg"));

        if (hit = Physics2D.Raycast(ShotStart, Vector2.left*FrontCheck, Mathf.Infinity,layerMask))  //총을 쏴서 맞으면
        {
            if (hit.collider.tag == "Player")
            {
                
                Vector2 backOff = new Vector2(FrontCheck*BackOff, 0);

                hit.collider.gameObject.GetComponent<PlayerController>().Damaged(damage);
                hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff);
            }
            hitPosition = hit.point;/*new Vector2(hit.point.x,firePoint.transform.position.y);*/
            Debug.Log(hit.collider.name);
            // Invoke("delayEffect", 0.3f);
            ShotEffect(hitPosition);
        }
        else
        {
            Debug.LogError("이게 안닿네;;");
        }
    }
    
    public void delayBlast()
    {
        Invoke("Blast",0.3f);
    }

    //여기서 불릿라인 생성
    void ShotEffect(Vector2 hitPosition)
    {
        bulletLine.SetPosition(0, firePoint.transform.position);
        bulletLine.SetPosition(1, hitPosition);

        bulletLine.enabled = true;
        Invoke("effectEnd",0.3f);
        hitPosition = Vector2.zero;
    }

    void effectEnd()
    {
        bulletLine.enabled = false;  
    }
}
