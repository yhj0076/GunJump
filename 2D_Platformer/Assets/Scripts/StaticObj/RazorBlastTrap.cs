using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorBlastTrap : MonoBehaviour
{
    Animator anime;
    LineRenderer razor;

    public GameObject firePoint;
    public int damage;
    public int dirIndex; // 0 : 동, 1 : 서, 2 : 남, 3 : 북
    public bool AlwaysBlast;

    float time;
    float StartTime;

    public float wait;
    public float idle;
    public float RUready;
    public float Blast;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        StartTime = 0;
        anime = GetComponent<Animator>();
        razor = GetComponent<LineRenderer>();
        razor.positionCount = 2;
        razor.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTime >= wait)
        {
            Nothing();
            Blaast();
            Ready();
        }
        else
        {
            StartTime += Time.deltaTime;
        }
    }

    void Blaast()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("Blast") && AlwaysBlast!=true)
        {
            time += Time.deltaTime;
            if (time >= Blast)
            {
                time = 0;
                anime.SetBool("Ready", false);
                anime.SetBool("Blast", false);
                razor.enabled = false;
            }
            else
            {
                RaycastHit2D hit;

                //총구 위치
                Vector2 ShotStart = new Vector2(firePoint.transform.position.x,
                                                firePoint.transform.position.y);
                Vector2 hitPosition = Vector2.zero;
                int layerMask = (-1) - (1 << LayerMask.NameToLayer("ThreeLeg"));

                Vector2 location = Vector2.zero;

                switch (dirIndex)
                {
                    case 0:
                        location = Vector2.right;
                        break;
                    case 1:
                        location = Vector2.left;
                        break;
                    case 2:
                        location = Vector2.down;
                        break;
                    case 3:
                        location = Vector2.up;
                        break;
                }

                if (hit = Physics2D.Raycast(ShotStart, location, Mathf.Infinity, layerMask))  //총을 쏴서 맞으면
                {
                    if (hit.collider.tag == "Player")
                    {

                        Vector2 backOff = Vector2.zero;
                        switch (dirIndex)
                        {
                            case 0:
                                backOff = Vector2.right * 30;
                                break;
                            case 1:
                                backOff = Vector2.left * 30;
                                break;
                            case 2:
                                backOff = Vector2.down * 30;
                                break;
                            case 3:
                                backOff = Vector2.up * 30;
                                break;
                        }
                        hit.collider.gameObject.GetComponent<PlayerController>().Damaged(damage);
                        hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff);
                    }
                    hitPosition = hit.point;
                    Debug.DrawLine(ShotStart,hitPosition);
                    razor.SetPosition(0, firePoint.transform.position);
                    razor.SetPosition(1, hitPosition);

                    razor.enabled = true;
                }
                else
                {
                    Debug.LogError("이게 안닿네;;");
                }
            }
        }
        else if(AlwaysBlast)
        {
            RaycastHit2D hit;

            //총구 위치
            Vector2 ShotStart = new Vector2(firePoint.transform.position.x,
                                            firePoint.transform.position.y);
            Vector2 hitPosition = Vector2.zero;
            int layerMask = (-1) - (1 << LayerMask.NameToLayer("ThreeLeg"));

            Vector2 location = Vector2.zero;

            switch (dirIndex)
            {
                case 0:
                    location = Vector2.right;
                    break;
                case 1:
                    location = Vector2.left;
                    break;
                case 2:
                    location = Vector2.down;
                    break;
                case 3:
                    location = Vector2.up;
                    break;
            }

            if (hit = Physics2D.Raycast(ShotStart, location, Mathf.Infinity, layerMask))  //총을 쏴서 맞으면
            {
                if (hit.collider.tag == "Player")
                {
                    Vector2 backOff = Vector2.zero;
                    switch (dirIndex)
                    {
                        case 0:
                            backOff = Vector2.right*30;
                            break;
                        case 1:
                            backOff = Vector2.left*30;
                            break;
                        case 2:
                            backOff = Vector2.down*30;
                            break;
                        case 3:
                            backOff = Vector2.up*30;
                            break;
                    }
                    hit.collider.gameObject.GetComponent<PlayerController>().Damaged(damage);
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff);
                }
                hitPosition = hit.point;
                razor.SetPosition(0, firePoint.transform.position);
                razor.SetPosition(1, hitPosition);

                razor.enabled = true;
            }
            else
            {
                Debug.LogError("이게 안닿네;;");
            }
        }
    }

    void Ready()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("BeforeBlast"))
        {
            time += Time.deltaTime;
            if (time >= RUready)
            {
                time = 0;
                anime.SetBool("Blast",true);
            }
        }
    }

    void Nothing()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("None"))
        {
            time += Time.deltaTime;
            if (time >= idle)
            {
                time = 0;
                anime.SetBool("Ready", true);
            }
        }
    }
}
