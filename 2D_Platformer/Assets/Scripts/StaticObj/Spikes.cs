using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage;
    public float BackOff;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 backOff = Vector2.zero;
            if (collision.contacts[0].normal.y < 0.7f)
            {
                backOff = new Vector2(0, -BackOff);
            }
            else
            {
                backOff = new Vector2(0, BackOff);
            }

            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff);   // 충돌시 튕겨나기
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 backOff = Vector2.zero;
            if (collision.contacts[0].normal.y > 0.7f)
            {
                backOff = new Vector2(0, -BackOff);
            }
            else
            {
                backOff = new Vector2(0, BackOff);
            }

            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff);    // 충돌시 튕겨나기
        }
    }
}
