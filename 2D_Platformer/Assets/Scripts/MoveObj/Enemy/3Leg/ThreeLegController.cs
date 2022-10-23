using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeLegController : MonoBehaviour
{
    ThreeLegAttack atk;
    SpriteRenderer sprite;
    Animator anime;
    Enemy enemy;
    Rigidbody2D rigid;
    LineRenderer blastLine;                 // 총의 궤적

    private AudioSource gunAudioPlayer;
    public AudioClip shotClip;

    float time;

    public float thinkingTime;              // 생각하는 시간
    public string NowState;                 // 현재 상태
    public float walkSp;                    // 걷는 속도
    public float jumpPow;                   // 점프 힘
    public GameObject BlastPos;             // 발사 위치
    private float lastThinkingTime = 0f;    // 마지막으로 생각한 시간
    public int frontCheck = 1;              // 전방 확인

    private bool onFloor = true;            

    Vector2 hitPosition = Vector2.zero;

    public enum THREELEG_STATE
    {
        TL_NONE,                // 아무것도 아님
        TL_WALK,                // 걷는 중
        TL_JUMP,                // 점프
        TL_JUMP_AFTER,          // 점프 후
        TL_ATTACK,              // 발사
        TL_ATK_AFTER,           // 공격 후
        TL_DEAD                 // 죽음
    }

    public THREELEG_STATE state
    {
        get;
        set;
    }

    // Start is called before the first frame update
    void Start()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        anime = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        rigid = GetComponent<Rigidbody2D>();
        blastLine = GetComponent<LineRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        atk = GetComponent<ThreeLegAttack>();
        state = THREELEG_STATE.TL_JUMP_AFTER;
    }

    // Update is called once per frame
    void Update()
    {
        flip();
        CheckWall();
        CheckCliff();
        CheckFloor();
        Jump();
        if ((state != THREELEG_STATE.TL_NONE||state!=THREELEG_STATE.TL_JUMP)&&onFloor)
        {
            Think();
            Walk();
            Attack();
        }

        if (enemy.health <= 0 || transform.position.y <= -20)
        {
            rigid.velocity = Vector2.zero;
            state = THREELEG_STATE.TL_DEAD;
            NowState = "Dead";
        }
    }

    void Think()
    {
        lastThinkingTime += Time.deltaTime;
        if (lastThinkingTime >= thinkingTime)
        {

            state = THREELEG_STATE.TL_ATTACK;
            NowState = "Attack";

            lastThinkingTime = 0;
        }
    }

    void Walk()
    {
        if(state == THREELEG_STATE.TL_WALK && anime.GetCurrentAnimatorStateInfo(0).IsName("3Leg_Walk"))
        {
            rigid.velocity = new Vector2(walkSp * frontCheck, rigid.velocity.y);
        }
    }
    
    void Attack()
    {
        if(state == THREELEG_STATE.TL_ATTACK)
        {
            rigid.velocity = Vector2.zero;
            atk.delayBlast();
            Invoke("SFX",0.3f);
            state = THREELEG_STATE.TL_ATK_AFTER;
        }
    }


    void SFX()
    {
        Vector3 viewPoint = Camera.main.WorldToViewportPoint(transform.position);
        if(Mathf.Abs(viewPoint.x) <= 1 &&
           Mathf.Abs(viewPoint.x) >= 0 &&
           Mathf.Abs(viewPoint.y) <= 1 &&
           Mathf.Abs(viewPoint.y) >= 0 &&
           Mathf.Abs(viewPoint.z) >= 1)
        gunAudioPlayer.PlayOneShot(shotClip);
    }

    void CheckWall()    // 전방에 벽이 있는가?
    {
        //int layerMask = 1 << LayerMask.NameToLayer("Platform");

        Debug.DrawRay(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y - 0.4f), Vector2.right * frontCheck * 0.2f, Color.red);
        Debug.DrawRay(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y + 0.4f), Vector2.right * frontCheck * 0.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y - 0.4f), Vector2.right * frontCheck, 0.2f);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(rigid.position.x + frontCheck * 0.5f, rigid.position.y + 0.4f), Vector2.right * frontCheck, 0.2f);
        if (hit || hit2)
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
            else if (hit2)
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

    void flip()
    {
        if (frontCheck > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        transform.GetChild(0).transform.localPosition = new Vector2(0.123f*frontCheck, 0.146f);
    }

    void CheckCliff()   // 전방에 절벽이 있는가?
    {
        Vector2 frontVec = new Vector2(rigid.position.x + (frontCheck * 0.6f), rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down * 1.5f, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(frontVec, Vector2.down * 1.5f, 1.2f, LayerMask.GetMask("Platform"));
        if (hit.collider == null && onFloor)    // 오른쪽으로 가고 있다면
        {
            // if (rigid.velocity.x > 0)
            // {
            //     frontCheck = -1;
            //     
            // }
            // else if (rigid.velocity.x < 0)
            // {
            //     frontCheck = 1;
            //     
            // }
            state = THREELEG_STATE.TL_JUMP;
            onFloor = false;
        }
    }

    void Jump()
    {
        if(state == THREELEG_STATE.TL_JUMP)
        {
            rigid.AddForce(new Vector2(walkSp * frontCheck, jumpPow), ForceMode2D.Impulse);
            lastThinkingTime = 2;
            state = THREELEG_STATE.TL_JUMP_AFTER;
        }
    }

    void CheckFloor()
    {
        Vector2 underVec = new Vector2(rigid.position.x, rigid.position.y);
        Debug.DrawRay(underVec, Vector2.down * 1.2f, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(underVec, Vector2.down, 1.2f, LayerMask.GetMask("Platform"));
        if (hit.collider != null)    // 오른쪽으로 가고 있다면
        {
            onFloor = true;
            state = THREELEG_STATE.TL_WALK;
        }
    }
}
