using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Controller : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anime;
    public enum B1_STATE
    {
        B1_NONE,
        B1_IDLE,
        B1_GRAB,
        B1_GRABBED,
        B1_MISSILE,
        B1_LAUNCHED,
        B1_DEAD,
        B1_ERROR
    }

    public B1_STATE state
    {
        get;
        set;
    }

    public float StartSpeed;

    float time = 0;
    public float thinkingTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = B1_STATE.B1_IDLE;
        time = 0;
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        rigid.velocity = Vector2.right * StartSpeed;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector2.zero;
        StopCoroutine("Move");
    }


    // Update is called once per frame
    void Update()
    {
        if (state != B1_STATE.B1_DEAD || state != B1_STATE.B1_ERROR)
        {
            Think();
            Grab();
            Missile();
        }
        else if(state == B1_STATE.B1_DEAD)
        {
            
        }
    }

    void Think()
    {
        if(state == B1_STATE.B1_IDLE)
        {
            time += Time.deltaTime;
            if(time>thinkingTime)
            {
                int choice = Random.Range(0,2);
                switch (choice)
                {
                    case 0:
                        state = B1_STATE.B1_GRAB;
                        break;
                    case 1:
                        state = B1_STATE.B1_MISSILE;
                        break;
                    default:
                        state = B1_STATE.B1_ERROR;
                        break;
                }
                time = 0;
            }
        }
    }

    void Grab()
    {
        if(state == B1_STATE.B1_GRAB)
        {
            Invoke("delayHand",0.47f);
            state = B1_STATE.B1_GRABBED;
            anime.SetBool("isGrab",true);
        }
    }

    void delayHand()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void Missile()
    {
        if(state == B1_STATE.B1_MISSILE)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            state = B1_STATE.B1_LAUNCHED;
            anime.SetBool("isMissile", true);
        }
    }
}
