using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Player_AnimationController : MonoBehaviour
{
    Animator anime;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    public float[] changeTime;
    float time = 0;
    bool GetGun = false;
    public GameObject exclamationMark;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime.SetInteger("BehaveIndex",-1);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        AnimationTimeControl();
        ExclamationMark();
        WalkRight();
        delayDisableObj();
    }

    void AnimationTimeControl()
    {
        if (time > changeTime[0] && time < changeTime[1])
        {
            anime.SetInteger("BehaveIndex", 0);
        }
        else if (time > changeTime[1] && time < changeTime[2])
        {
            anime.SetInteger("BehaveIndex", 1);
        }
        else if (time > changeTime[2] && time < changeTime[3])
        {
            anime.SetInteger("BehaveIndex", 2);
        }
        else if (time > changeTime[3] && time < changeTime[4])
        {
            anime.SetInteger("BehaveIndex", 3);
        }
        else if (time > changeTime[4])
        {
            anime.SetInteger("BehaveIndex", 4);
        }
        else
        {
            anime.SetInteger("BehaveIndex", -1);
        }
    }
    void ExclamationMark()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("NPC_Player_Idle"))
        {
            exclamationMark.SetActive(true);
        }
        else
        {
            exclamationMark.SetActive(false);
        }
    }
    void WalkRight()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("NPC_Player_Walk")|| 
            anime.GetCurrentAnimatorStateInfo(0).IsName("NPC_Player_WalkWithGun")||
            anime.GetCurrentAnimatorStateInfo(0).IsName("NPC_Player_Walk 0"))
        {
            rigid.velocity = new Vector2(5,0);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
    }

    void delayDisableObj()
    {
        if(time > changeTime[2] && time < changeTime[3] && GetGun == false)
        {
            Invoke("clearObj",(changeTime[3]-changeTime[2])/3);
            Invoke("returnObj",changeTime[3]-changeTime[2]);
            GetGun = true;
        }
    }

    void clearObj()
    {
        spriteRenderer.color = Color.clear;
    }

    void returnObj()
    {
        spriteRenderer.color = Color.white;
    }
}
