using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Doc_Control : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anime;

    public GameObject Destroy_Player;

    float time = 0;
    public float WaitTime;
    public float WaitTime2;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= WaitTime&&Destroy_Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            rigid.velocity = new Vector2(-Speed,0);
            spriteRenderer.flipX = true;
            anime.SetBool("isWalk",true);
        }
        if(time >= WaitTime2)
        {
            GameManager.instance.SelectScene(1);
        }
    }
}
