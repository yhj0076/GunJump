using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float JumpPower;
    public int MaxJump;
    public float MaxDistance;
    //public TalkManager talkManager;

    public bool isGrounded = false;
    public bool DontMove = false;
    public bool StageClear = false;
    public bool inBoss1Stage = false;

    public int health = 100;
    
    public Vector2 MousePosition;
    public Vector2 PlayerPosition;

    public string Now = "None";

    bool isHurt = false;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public Animator anime;
    new Camera camera;

    public bool isDoc = false;
    public enum PLAYER_STATE
    {
        PS_NONE,
        PS_POWWWER,
        PS_DIE
    }

    public PLAYER_STATE state
    {
        get;
        private set;
    }

    void Awake()
    {
        isHurt = false;
        StageClear = false;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        state = PLAYER_STATE.PS_NONE;
        health = PlayerPrefs.GetInt("HEALTH");
        if(health<=0)
        {
            health = 100;
        }
    }

    private void Update()
    {
        if (DontMove==false)
        {
            Move();
        }
        else
        {
            anime.SetBool("isJump", false);
            anime.SetBool("isWalk", false);
            rigid.velocity = Vector2.zero;
        }

        //Direction Sprite
        if (UIManager.instance.pause < 0&&UIManager.instance.Dead==false)
        {
            if (StageClear == false)
            {
                spriteRenderer.flipX = MousePosition.x < 0;

                if (isDoc)
                {
                    if (spriteRenderer.flipX)
                        transform.GetChild(0).transform.localPosition = new Vector3(0.13f, 0.031f, 0);
                    else
                        transform.GetChild(0).transform.localPosition = new Vector3(-0.13f, 0.031f, 0);
                }
                else
                {
                    if (spriteRenderer.flipX)
                        transform.GetChild(0).transform.localPosition = new Vector3(0.117f, -0.072f, 0);
                    else
                        transform.GetChild(0).transform.localPosition = new Vector3(-0.117f, -0.072f, 0);
                }
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        if(anime.GetCurrentAnimatorStateInfo(0).IsName("Player_Dead"))
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            EffectComeback();
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (DontMove == false)
        {
            //Move speed
            float h = Input.GetAxisRaw("Horizontal");

            rigid.AddForce(Vector2.right*h, ForceMode2D.Impulse);

            //Max speed
            if (isHurt==false)
            {
                if (rigid.velocity.x > maxSpeed)
                {
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                }
                else if (rigid.velocity.x < maxSpeed * (-1))
                {
                    rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
                }
            }
        }

        if(StageClear)
        {
            rigid.velocity = new Vector2(maxSpeed,0);
            PlayerPrefs.SetInt("HEALTH", health);
        }
    }

    public void GetPlayerInfo()
    {
        // 플레이어 좌표와 마우스 좌표를 동시에 얻습니다.
        GetPlayerPosition();
        GetMousePosition();
    }

    public void GetPlayerPosition()
    {
        // 플레이어 좌표 얻기
        PlayerPosition = this.gameObject.transform.position;
        //PlayerPosition = camera.WorldToScreenPoint(PlayerPosition);
    }

    public void GetMousePosition()
    {
        // 마우스 좌표 얻기
        MousePosition = Input.mousePosition;
        MousePosition = camera.ScreenToWorldPoint(MousePosition)-transform.position;

    }
    
    public void Move()
    {
        if (DontMove == false)
        {
            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                rigid.velocity = Vector2.zero;
                rigid.AddForce(new Vector2(rigid.velocity.x, JumpPower));
            }


            //Stop speed
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }


            

            //walking anime

            if (Mathf.Abs(rigid.velocity.x) > 0.2f||inBoss1Stage)
            {
                anime.SetBool("isWalk", true);
            }
            else
            {
                anime.SetBool("isWalk", false);
            }
        }
    }
    
    public void recoil(float Recoil)
    {
        rigid.AddForce(new Vector2(MousePosition.normalized.x * Recoil * (-1), MousePosition.normalized.y * Recoil * (-1)), ForceMode2D.Impulse);
    }

    public void Damaged(int power)
    {
        if (state != PLAYER_STATE.PS_POWWWER)
        {
            isHurt = true;
            state = PLAYER_STATE.PS_POWWWER;
            health -= power;
            UIManager.instance.Health(health);
            if (health <= 0)
            {
                state = PLAYER_STATE.PS_DIE;
                Die();
            }
            StartCoroutine("hurtEffect");
            Invoke("EffectComeback", 1);
        }
    }

    void Die()
    {
        UIManager.instance.GameOverUI();
        Now = "Dead";
        anime.SetBool("isDead", true);
    }

    IEnumerator hurtEffect()
    {
        transform.GetComponent<SpriteRenderer>().material.color = Color.red;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material.color = Color.red;
        anime.SetBool("isDamaged", true);
        yield return new WaitForSeconds(0.03f);
        isHurt = false;
        anime.SetBool("isDamaged", false);
        transform.GetComponent<SpriteRenderer>().material.color = Color.gray;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material.color = Color.gray;
    }

    public void corZonya()
    {
        StartCoroutine("zonyaEffect");
    }

    IEnumerator zonyaEffect()
    {
        DontMove = true;
        transform.GetComponent<SpriteRenderer>().material.color = Color.yellow;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material.color = Color.yellow;
        anime.SetBool("Recoil", true);
        yield return new WaitForSeconds(2f);
        DontMove = false;
        anime.SetBool("Recoil", false);
        EffectComeback();
    }

    void EffectComeback()
    {
        state = PLAYER_STATE.PS_NONE;
        transform.GetComponent<SpriteRenderer>().material.color = Color.white;
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().material.color = Color.white;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            //UIManager.instance.GameOverUI();
            Time.timeScale = 0;
        }
    }
}

