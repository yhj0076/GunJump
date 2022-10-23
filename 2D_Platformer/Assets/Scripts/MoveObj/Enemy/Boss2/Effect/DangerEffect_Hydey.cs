using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 경고와 레이저를 한꺼번에 관리
// 활성화 시 anime.SetBool("isAttack",true)를 사용할 것
public class DangerEffect_Hydey : MonoBehaviour
{
    BoxCollider2D box;
    Animator anime;

    public float recoilForceX;
    public float recoilForceY;
    public int damage;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anime = GetComponent<Animator>();
        anime.SetBool("isAttack",false);
    }

    // Update is called once per frame
    void Update()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("Razor_Blast"))
        {
            box.enabled = true;
        }
        else
        {
            box.enabled = false;
        }

        if(anime.GetCurrentAnimatorStateInfo(0).IsName("Razor_End"))
        {
            anime.SetBool("isAttack",false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-recoilForceX, recoilForceY), ForceMode2D.Impulse);
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(recoilForceX, recoilForceY), ForceMode2D.Impulse);
            }
            collision.GetComponent<PlayerController>().Damaged(damage);
        }
    }   //데미지 계산
}
