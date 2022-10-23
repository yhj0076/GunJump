 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    CapsuleCollider2D col;
    Animator anime;
    Enemy enemy;


    public GameObject SlashP;


    public float thinkingTime;              // 생각하는 시간
    public float moveDistance;              //움직이는 거리
    public string NowState;                // 현재 상태
    private float lastThinkingTime = 0f;    // 마지막으로 생각한 시간
    public int frontCheck = 1;            // 전방 확인

    public bool Searched = false;
    public enum REAPER_STATE
    {
        RS_NONE,                // 아무것도 아님
        RS_IDLE,                // 대기 상태
        RS_TELEPORT,            // 텔레포트
        RS_ATTACK,              // 공격
        RS_BEFORETHINKFORNT,    // 앞뒤 생각하기
        RS_DEAD                 // 죽음
    }

    public REAPER_STATE state
    {
        get;
        set;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
        col = GetComponent<CapsuleCollider2D>();
        anime = GetComponent<Animator>();
        state = REAPER_STATE.RS_IDLE;
        MovePoint();
    }

    // Update is called once per frame
    void Update()
    {
        flip();
        if (state != REAPER_STATE.RS_NONE)
        {
            Think();
            Attack();
            Teleporting();
        }

        if (enemy.health <= 0)
        {
            state = REAPER_STATE.RS_DEAD;
            NowState = "Dead";
        }
    }

    void Think()
    {
        if (Time.time >= lastThinkingTime + thinkingTime)
        {
            int choice = Random.Range(0, 3);
            switch (choice)
            {
                case 0:
                    state = REAPER_STATE.RS_IDLE;
                    NowState = "Idle";
                    break;
                case 1:
                    state = REAPER_STATE.RS_ATTACK;
                    NowState = "Attack";
                    break;
                case 2:
                    state = REAPER_STATE.RS_TELEPORT;
                    NowState = "Teleport";
                    break;
                default:
                    break;
            }
            lastThinkingTime = Time.time;
        }
    }

    void Attack()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("Ripper_Slash"))
        {
            Slash();
        }
    }

    void Slash()
    {
        transform.position = Vector2.MoveTowards(transform.position, SlashP.transform.position, 50 * Time.deltaTime);
        Invoke("MovePoint",thinkingTime/3);
    }

    void MovePoint()
    {
        SlashP.transform.position = new Vector2(rigid.position.x + moveDistance*frontCheck, rigid.position.y);
        if (Searched==false)
        {
            frontThink();
        }
    }

    void Teleporting()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("Ripper_Teleport2"))
        {
            transform.position = SlashP.transform.position;
            Invoke("MovePoint", thinkingTime / 2);
        }
    }

    void frontThink()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + moveDistance*frontCheck, rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down * 2, Color.blue);
        Debug.DrawRay(new Vector2(SlashP.transform.position.x+frontCheck*1.5f, SlashP.transform.position.y), transform.position - SlashP.transform.position, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(frontVec, Vector2.down * 2, 2, LayerMask.GetMask("Platform"));
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(SlashP.transform.position.x + frontCheck*1.5f, SlashP.transform.position.y), transform.position - SlashP.transform.position);
        if (hit.collider == null|| hit2.collider.tag == "Platform")
        {
            frontCheck = frontCheck * -1;
        }
        else if (state == REAPER_STATE.RS_BEFORETHINKFORNT)
        {
            int choice = Random.Range(0, 2);
            switch (choice)
            {
                case 0:
                    frontCheck = 1;
                    break;
                case 1:
                    frontCheck = -1;
                    break;
                default:
                    break;
            }
            state = REAPER_STATE.RS_IDLE;
            NowState = "Idle";
        }
    }

    void flip()
    {
        if(frontCheck<0)
        {
            sprite.flipX = true;
            col.offset = new Vector2(0.25f,-0.3f);
        }
        else
        {
            sprite.flipX = false;
            col.offset = new Vector2(-0.25f, -0.3f);
        }
    }
}
