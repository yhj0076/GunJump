using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public int damage;
    public float Recoil;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Damaged(damage);
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,Recoil),ForceMode2D.Impulse);
        }
    }
}
