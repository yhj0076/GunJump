using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkControl : MonoBehaviour
{
    BoxCollider2D box;
    Animator anime;
    float time = 0;
    public float SparkTime;
    public int dmg;
    void Start()
    {
        anime = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("SparkDown"))
        {
            time += Time.deltaTime;
            if(time > SparkTime)
            {
                anime.SetBool("Sparkling",true);
                time = 0;
            }

            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
            {
                box.enabled = false;
            }
        }
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("SparkUp"))
        {
            anime.SetBool("Sparkling",false);
            box.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Damaged(dmg);
            if(transform.localScale.x > 0)
            {
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 20),ForceMode2D.Impulse);
            }
            else
            {
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 20),ForceMode2D.Impulse);
            }
        }
    }
}
