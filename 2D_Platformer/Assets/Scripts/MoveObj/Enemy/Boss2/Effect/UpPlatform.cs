using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpPlatform : MonoBehaviour
{
    Rigidbody2D rigid;

    public bool Cleared;
    public float speed;
    bool Godown;
    private void Start()
    {
        Cleared = false;
        Godown = false;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position.y<=-6.35f&&Godown)
        {
            rigid.velocity = Vector2.zero;
            Godown = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Cleared)
        {
            rigid.velocity = Vector2.up*speed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(Cleared)
        {
            Godown = true;
            rigid.velocity = Vector2.down*speed;
        }
    }
}
