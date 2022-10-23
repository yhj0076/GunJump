using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimation_Hydey : MonoBehaviour
{
    Animator anime;
    SpriteRenderer spriteRenderer;
    float time = 0;
    public float ThinkingTime;
    bool Razor = false;

    public bool startGame = false;
    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        time = 0;
        anime.SetInteger("AtkIndex", -1);
        Razor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            if (transform.parent.GetComponent<Animator>().GetBool("isDead"))
            {
                gameObject.SetActive(false);
            }

            if (anime.GetCurrentAnimatorStateInfo(0).IsName("Arm_Idle"))
            {
                anime.SetInteger("AtkIndex", -1);
                time += Time.deltaTime;
                if (time >= ThinkingTime)
                {
                    Razor = false;
                    anime.SetInteger("AtkIndex", Random.Range(0, 6));
                    //anime.SetInteger("AtkIndex", 0);
                    if (anime.GetInteger("AtkIndex") == 0)
                    {
                        transform.GetChild(2).GetComponent<ArmRLAtk_Controller>().DoIt = true;
                        transform.GetChild(3).GetComponent<ArmRLAtk_Controller>().DoIt = true;
                    }
                    else if(anime.GetInteger("AtkIndex") == 3)
                    {
                        Invoke("delayGunRazor",0.3f);
                    }
                    time = 0;
                }

                if (transform.parent.GetComponent<SpriteRenderer>().flipX)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }

            if (anime.GetCurrentAnimatorStateInfo(0).IsName("Arm_RazorAtk"))
            {
                Invoke("RazorOn", 0.3f);
            }

            if(anime.GetCurrentAnimatorStateInfo(0).IsName("Arm_RAtk")||anime.GetCurrentAnimatorStateInfo(0).IsName("Arm_LAtk"))
            {
                transform.GetChild(4).gameObject.SetActive(true);
                if (anime.GetCurrentAnimatorStateInfo(0).IsName("Arm_RAtk"))
                {
                    transform.GetChild(4).GetComponent<DmgZone>().RL = false;
                }
                else if (anime.GetCurrentAnimatorStateInfo(0).IsName("Arm_LAtk"))
                {
                    transform.GetChild(4).GetComponent<DmgZone>().RL = true;
                }
            }
            else
            {
                transform.GetChild(4).gameObject.SetActive(false);
            }

            if(anime.GetCurrentAnimatorStateInfo(0).IsName("Arm_dashWithHydey"))
            {
                GetComponent<Boss2_Health>().canHit = false;
            }
        }
    }

    void delayGunRazor()
    {
        transform.GetChild(5).gameObject.SetActive(true);
    }

    void RazorOn()
    {
        if(Razor == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Razor = true;
        }
    }
}
