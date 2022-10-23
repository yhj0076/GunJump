using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour
{
    Rigidbody2D rigid;
    public bool StageStart;
    public float Speed;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        StageStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(StageStart)
        {
            rigid.velocity = Vector2.left*Speed;
            if(transform.position.x<=-4)
            {
                transform.position = new Vector3(transform.position.x+4,transform.position.y,transform.position.z);
            }
        }
    }
}
