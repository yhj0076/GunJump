using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour
{
    public float runSpeed;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        GoBack();   
    }
    void Move()
    {    
        rigid.velocity = Vector2.left * runSpeed;
    }

    void GoBack()
    {
        Vector2 backPos = new Vector2(-4, 0);
        if (rigid.position == backPos)
        {
            rigid.position = Vector2.zero;
        }
    }
}
