using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deliver_Move : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    public bool RUN;
    public float speed;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        RUN = false;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;
        Player = FindObjectOfType<NPC_Player_AnimationController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(RUN)
        {
            rigid.velocity = Vector2.Lerp(rigid.velocity, new Vector2(speed,0),0.005f);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }

        if(Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("NPC_Player_WalkWithGun"))
        {
            spriteRenderer.flipX = true;
            RUN = true;
        }
    }
}
