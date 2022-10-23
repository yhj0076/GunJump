using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Boss1Damage enemy;
    public GameObject Boss;
    public GameObject Missile;
    public GameObject StopPos;
    public GameObject GrabHand;

    private AudioSource Boss1AudioPlayer;
    public AudioClip beforeGrabClip;
    public AudioClip MissileLaunchClip;
    public AudioClip afterGrabClip;

    /* 보스1 패턴
     * 1. 대기상태(그냥 대기)
     * 2. 움직이는 상태(그냥 움직임)
     * 3. 미사일 발사(랜덤한 위치로 발사)
     * 4. 그랩(플레이어 방향으로 그랩)
     */
    public enum BOSS1_STATE
    {
        ES_NONE,            // 무상태
        ES_IDLE,            // 대기 상태
        ES_LAUNCHING,      // 미사일 발사 전 상태
        ES_LAUNCHED,          // 미사일 발사 후 상태
        ES_GRAB,             // 돌진상태
        ES_DEAD
    }

    public BOSS1_STATE state
    {
        get;
        set;
    }

    

    public float thinkingTime;              // 생각하는 시간
    public float walkSpeed;                 // 걷는 속도
    public bool isSplit;
    public string NowState;                // 현재 상태
    public float lastThinkingTime = 5f;    // 마지막으로 생각한 시간
    public int frontCheck = -1;            // 전방 확인

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Boss1Damage>();
        sprite = GetComponent<SpriteRenderer>();
        Boss1AudioPlayer = GetComponent<AudioSource>();
        state = BOSS1_STATE.ES_IDLE;
        NowState = "NONE";
    }


    
    // Update is called once per frame
    void Update()
    {
        Die();
        if (state != BOSS1_STATE.ES_NONE && state != BOSS1_STATE.ES_DEAD)
        {
            Think();
            Launch();
            Idle();
            Grab();
        }


        if(enemy.health <= 0)
        {
            state = BOSS1_STATE.ES_DEAD;
        }
    }
    
    void Die()
    {
        if(state == BOSS1_STATE.ES_DEAD)
        {
            rigid.velocity = Vector2.right * 5;
            NowState = "으앙 쥬금";
        }
    }

    void Think()
    {
        if (state != BOSS1_STATE.ES_DEAD)
        {
            lastThinkingTime += Time.deltaTime;
            if (lastThinkingTime >= thinkingTime && state != BOSS1_STATE.ES_DEAD)
            {
                int choice = Random.Range(0, 2);
                switch (choice)
                {
                    case 0:
                        state = BOSS1_STATE.ES_LAUNCHING;   // 1/2의 확률로 미사일을 발사한다.
                        NowState = "LAUNCHING";
                        break;
                    case 1:
                        state = BOSS1_STATE.ES_GRAB;       // 1/2의 확률로 그랩한다.
                        NowState = "GRAB";
                        break;
                    default:
                        break;
                }
                lastThinkingTime = 0;
            }
        }
    }

    void Launch()
    {
        if (state == BOSS1_STATE.ES_LAUNCHING)
        {
            GameObject.Find("Missile Launcher").GetComponent<MissileController>().Fall();
            LaunchSound();
            GameObject.Find("Missile Launcher").GetComponent<MissileController>().Invoke("Fall", 0.5f);
            Invoke("LaunchSound", 0.25f);
            GameObject.Find("Missile Launcher").GetComponent<MissileController>().Invoke("Fall", 1f);
            Invoke("LaunchSound", 0.5f);
            state = BOSS1_STATE.ES_LAUNCHED;
            NowState = "LAUNCHED";
        }
    }

    void LaunchSound()
    {
        Boss1AudioPlayer.PlayOneShot(MissileLaunchClip);
    }

    void Idle()
    {
        if(state == BOSS1_STATE.ES_IDLE)
        {
            rigid.velocity = Vector2.zero;
            // NowState = "IDLE";
        }
    }

    void Grab()
    {
        if(state == BOSS1_STATE.ES_GRAB)
        {
            Boss1AudioPlayer.PlayOneShot(beforeGrabClip);
            Invoke("GrabStart", 0.8f);          
        }
    }

    void GrabStart()
    {
        GetComponentInChildren<BossGrabHand>().Shrink = false;
        Boss1AudioPlayer.PlayOneShot(afterGrabClip);
    }

}
