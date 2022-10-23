using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStage3_1BackGround : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-18)
        {
            transform.position = new Vector2(0,18);
        }
    }
}
