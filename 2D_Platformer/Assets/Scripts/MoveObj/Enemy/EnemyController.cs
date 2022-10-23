using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.15
 * 최종 수정자 : 이호준
 * 파일 이름 : EnemyController.cs
 * 클래스 내용 : Enemy의 인공지능을 책임지는 스크립트
 */

public class EnemyController : MonoBehaviour    // 적의 상태 및 이동 AI
{
    Animator anime;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Enemy enemy;

    public enum ENEMY_STATE
    {
        ES_NONE,            // 무상태
        ES_IDLE,            // 대기 상태
        ES_WALKING,         // 걷는 중 상태
        ES_BEFOREWALK,      // 걷기 전 생각
        ES_ATTACK,          // 공격 상태
        ES_HIT,             // 피격 상태
        ES_DIE              // 사망 처리 상태
    }



    public ENEMY_STATE state
    {
        get;
        set;
    }


    public float walkSpeed;          // 걷는 속도
    public float thinkingTime = 5.0f;       // 생각하는 시간
    public float WakeUpTimePercent = 50f;   // 깨어나는 시간 퍼센트
    public float lastThinkingTime = 0f;     // 마지막으로 생각한 시간.
    public bool cliffCheck = false;         // 전방에 절벽이 있으면 뒤돈다.
    public int lastState = 0;

    public int frontCheck = -1;
    private bool splitX = false;    // T == 오른쪽을 바라본다., F == 왼쪽을 바라본다.
    public bool isSplit = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
        anime = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            Destroy(gameObject);
        }

        Attack();
        Thinking();
        if (cliffCheck)
        {
            CheckCliff();
        }
        CheckWall();
        Walk_Think();
        Walk();
        Stop();
        Hit();
        spriteRenderer.flipX = splitX;
    }

    void Thinking()
    {
        if (state == ENEMY_STATE.ES_HIT&& state!=ENEMY_STATE.ES_ATTACK)       // 맞으면 생각하는 시간의 WakeUpTimePercent%의 시간이 지난 후 정신차림.
        {
            lastThinkingTime = Time.time-(thinkingTime*(WakeUpTimePercent/100));
        }
        else if (Time.time >= lastThinkingTime + thinkingTime && state != ENEMY_STATE.ES_ATTACK)
        {
            Think();
            lastThinkingTime = Time.time;
        }
    }

    void Think()    // 걸을 지 멈출 지 결정
    {
        int choice = Random.Range(0, 5);
        switch (choice)
        {
            case 0:
                state = ENEMY_STATE.ES_IDLE;
                lastState = 1;
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                state = ENEMY_STATE.ES_BEFOREWALK;
                lastState = 2;
                break;
            default:
                break;
        }
    }

    void Walk_Think()
    {
        if(state == ENEMY_STATE.ES_BEFOREWALK && state != ENEMY_STATE.ES_ATTACK)
        {
            int choice = Random.Range(0, 2);
            switch (choice)
            {
                case 0:
                    frontCheck = -1;
                    break;
                case 1:
                    frontCheck = 1;
                    break;
                default:
                    break;
            }
            state = ENEMY_STATE.ES_WALKING;
        }
    }

    void Walk()
    {
        if (state == ENEMY_STATE.ES_WALKING && state != ENEMY_STATE.ES_ATTACK)
        {
            rigid.velocity = new Vector2(walkSpeed * frontCheck, rigid.velocity.y);

            //Max speed
            if (rigid.velocity.x > 0)   // 오른쪽으로 갈 때
            {
                splitX = true;
                isSplit = true;
            }
            else if (rigid.velocity.x < 0)      // 왼쪽으로 갈 때
            {
                splitX = false;
                isSplit = false;
            }
        }
    }

    void Stop()
    {
        if(state== ENEMY_STATE.ES_IDLE && state != ENEMY_STATE.ES_ATTACK)
        {
            rigid.velocity = Vector2.zero;
        }
    }

    void Hit()
    {
        if(state == ENEMY_STATE.ES_HIT && state != ENEMY_STATE.ES_ATTACK)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y);
            state = ENEMY_STATE.ES_NONE;
        }
    }

    void CheckCliff()   // 전방에 절벽이 있는가?
    {
        Vector2 frontVec = new Vector2(rigid.position.x + (frontCheck*0.6f), rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down*2, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(frontVec, Vector2.down*2,2, LayerMask.GetMask("Platform"));
        if(hit.collider == null)    // 오른쪽으로 가고 있다면
        {
            if (rigid.velocity.x > 0)
            {
                frontCheck = -1;
                Walk();
            }
            else if(rigid.velocity.x<0)
            {
                frontCheck = 1;
                Walk();
            }
        }
    }

    void CheckWall()    // 전방에 벽이 있는가?
    {
        Debug.DrawRay(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y - 0.4f), Vector2.right * frontCheck * 0.2f, Color.red);
        Debug.DrawRay(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y + 0.4f), Vector2.right * frontCheck * 0.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y - 0.4f), Vector2.right * frontCheck, 0.2f);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y + 0.4f), Vector2.right * frontCheck, 0.2f);
        if (hit||hit2)
        {
            if (hit)
            {
                if (hit.collider != null)
                {
                    if (rigid.velocity.x > 0)
                    {
                        frontCheck = -1;
                        Walk();
                    }
                    else if (rigid.velocity.x < 0)
                    {
                        frontCheck = 1;
                        Walk();
                    }
                }
            }
            else if(hit2)
            {
                if (hit2.collider != null)
                {
                    if (rigid.velocity.x > 0)
                    {
                        frontCheck = -1;
                        Walk();
                    }
                    else if (rigid.velocity.x < 0)
                    {
                        frontCheck = 1;
                        Walk();
                    }
                }
            }
        }
    }

    void Attack()
    {
        if (state == ENEMY_STATE.ES_ATTACK)
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
