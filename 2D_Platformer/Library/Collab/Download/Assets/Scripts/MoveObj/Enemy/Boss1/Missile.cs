using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Animator anime;
    Rigidbody2D rigid;
    PolygonCollider2D col;
    AudioSource boom;
    public AudioClip explode;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        boom = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.transform.tag == "Platform")
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            col.enabled = false;
            anime.SetBool("Boom", true);
            boom.PlayOneShot(explode);
            Destroy(transform.gameObject, 1.05f);
        }
        else if(collision.transform.tag == "Player")
        {            
            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
            col.enabled = false;
            anime.SetBool("TouchBoom", true);
            boom.PlayOneShot(explode);
            Destroy(transform.gameObject, 1.05f);
        }
    }
}
