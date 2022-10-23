using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsDestroy : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * FindObjectOfType<InStage3_1BackGround>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-16)
        {
            Destroy(gameObject);
        }
    }
}
