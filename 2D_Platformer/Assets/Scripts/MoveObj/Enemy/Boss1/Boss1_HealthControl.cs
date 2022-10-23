using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_HealthControl : MonoBehaviour
{
    Boss1_Controller boss1con;

    public GameObject ObsGen;
    public int health;
    public int damage;
    public int BackOff;

    // 주인공을 보면 공격을 한다.
    // 시간은 n초마다 공격할 수 있게 만든다.

    private void Awake()
    {
        boss1con = GetComponent<Boss1_Controller>();
    }

    public void Damaged(int power)
    {    
        // switch (this.gameObject.name)
        // {
        //     case "Ripper":
        //     case "3LegTurret":
        //         break;
        //     default:
        //         enemyController.state = EnemyController.ENEMY_STATE.ES_HIT;
        //         break;
        // }
        if (health <= 0)
        {
            boss1con.state = Boss1_Controller.B1_STATE.B1_DEAD;
            FindObjectOfType<GridMover>().StageStart = false;
            ObsGen.SetActive(false);
            //Destroy(this);
        }
        else
        {
            health -= power;
            StartCoroutine("DamageEffect");
        }
    }

    IEnumerator DamageEffect()
    {
        transform.GetComponent<SpriteRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.03f);
        transform.GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)  // 플레이어와 적이 충돌했을 시 플레이어에게 데미지
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 checkX = collision.gameObject.transform.position - transform.position;

            int checkSide = 1;
            // if (checkX.x > 0)
            // {
            //     checkSide = 1;
            // }
            // else
            // {
            //     checkSide = -1;
            // }
            Vector2 backOff = new Vector2(BackOff * checkSide, 10);
            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = backOff;    // 충돌시 튕겨나기
        }
    }
}
