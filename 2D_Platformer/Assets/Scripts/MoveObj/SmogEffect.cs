using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogEffect : MonoBehaviour
{
    public int speed;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rigid.velocity = Vector2.left*speed;
    }
}
