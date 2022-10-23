using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgZone : MonoBehaviour
{
    BoxCollider2D box;
    public int damage;
    public float BackOff;
    public bool RL; // L == true R == false
    void OnEnable()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        if(transform.parent.GetComponent<SpriteRenderer>().flipX!=RL)
        {
            transform.localPosition = new Vector2(-1.54f,-0.38f);
        }
        else
        {
            transform.localPosition = new Vector2(1.54f,-0.38f);
        }
        Invoke("delayBoxOn",0.3f);
    }
    
    void delayBoxOn()
    {
        box.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Vector3 checkX = collision.gameObject.transform.position - transform.position;

            int checkSide = 1;
            if (checkX.x > 0)
            {
                checkSide = 1;
            }
            else
            {
                checkSide = -1;
            }
            Vector2 backOff = new Vector2(BackOff * checkSide, 0);
            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff, ForceMode2D.Impulse);    // 충돌시 튕겨나기
        }
    }
}
