using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 보스2의 메인 부분

public class BodyAnimation_Hydey : MonoBehaviour
{
    public GameObject Player;
    public float dashPower;
    Animator anime;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    bool heyst = false;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        heyst = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.GetChild(0).gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Arm_ShotWithHydey")||
           transform.GetChild(0).gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Arm_dashWithHydey"))
        {
            anime.SetBool("withHydey", true);
        }
        else
        {
            anime.SetBool("withHydey", false);
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            heyst = false;
        }

        if(transform.GetChild(0).gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Arm_Idle"))
        {
            if (Player.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        if (transform.GetChild(0).gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Arm_dashWithHydey"))
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (heyst == false)
            {
                if(spriteRenderer.flipX)
                {
                    rigid.AddForce(new Vector2(-dashPower,0),ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(new Vector2(dashPower,0),ForceMode2D.Impulse);
                }
                heyst = true;
            }
        }
    }
}
