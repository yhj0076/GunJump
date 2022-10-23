using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Health : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public int health;
    public int damage;
    public int BackOff;

    public bool canHit;

    public GameObject Corpse;

    Animator anime;
    // 주인공을 보면 공격을 한다.
    // 시간은 n초마다 공격할 수 있게 만든다.

    private void Awake()
    {
        anime = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Damaged(int power)
    {
        if (canHit)
        {
            health -= power;
            if (health <= 0)
            {
                PlayerPrefs.SetFloat("timeScale",0.2f);
                anime.SetBool("isDead",true);
                Invoke("delayTimeRecovery",0.4f);
            }
            StartCoroutine("DamageEffect");
        }
    }

    void delayTimeRecovery()
    {
        PlayerPrefs.SetFloat("timeScale",1);
        GameObject.Instantiate(Corpse, gameObject.transform.position, Quaternion.identity).GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
        FindObjectOfType<UpPlatform>().Cleared = true;
        Destroy(gameObject);
    }

    IEnumerator DamageEffect()
    {
        transform.GetComponent<SpriteRenderer>().material.color = Color.red;
        transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.03f);
        transform.GetComponent<SpriteRenderer>().material.color = Color.white;
        transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)  // 플레이어와 적이 충돌했을 시 플레이어에게 데미지
    {
        if (collision.gameObject.tag == "Player")
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
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff,ForceMode2D.Impulse);    // 충돌시 튕겨나기
        }
    }
}
