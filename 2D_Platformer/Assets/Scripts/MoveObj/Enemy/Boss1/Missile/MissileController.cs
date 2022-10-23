using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public int damage;
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        if(collision.transform.tag == "Player")
        {
            GetComponent<Animator>().SetBool("isPlayer",true);
            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
        }
        else/* if(collision.transform.tag == "Platform")*/
        {
            GetComponent<Animator>().SetBool("isFloor",true);
            Debug.Log("쾅!");
        }
    }
}
