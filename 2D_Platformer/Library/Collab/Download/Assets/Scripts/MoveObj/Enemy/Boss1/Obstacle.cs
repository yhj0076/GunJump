using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D rigid;
    bool SMASH;
    // Start is called before the first frame update
    void Start()
    {
        SMASH = false;
        rigid = GetComponent<Rigidbody2D>();
        gameObject.tag = "Platform";
        gameObject.layer = 16;
    }

    // Update is called once per frame
    void Update()
    {
        if (SMASH)
        {
            
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        rigid.velocity = Vector2.left * GameObject.Find("Tilemap for BackGround").GetComponent<GridMover>().runSpeed*1.2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Boss1")
        {
            SMASH = true;
            Destroy(transform.gameObject,2);
            rigid.gravityScale = 4;
            rigid.mass = 1;
            rigid.AddForce(new Vector2(-10,100));
            if (GetComponent<PolygonCollider2D>() == null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
    }
}
