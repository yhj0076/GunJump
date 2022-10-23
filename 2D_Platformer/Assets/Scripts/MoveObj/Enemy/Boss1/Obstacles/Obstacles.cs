using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float speed = FindObjectOfType<GridMover>().Speed;
        GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.name == "Boss1")
        {
            Debug.Log("부딪혔다");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-3,3);
            //GetComponent<Collider2D>().isTrigger = true;
            transform.gameObject.layer = 19;
            Destroy(gameObject,3f);
        }
    }
}
