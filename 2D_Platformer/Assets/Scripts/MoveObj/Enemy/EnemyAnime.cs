using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnime : MonoBehaviour // 적 애니메이션 매니저
{
    EnemyController enemyController;
    Rigidbody2D rigid;
    Animator anime;
    Collider2D col;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        col = GetComponent<Collider2D>();
        if(gameObject.name == "Cleaner")
        anime.SetBool("isDead", false);
    }

    void Update()
    {
        if (Mathf.Abs(rigid.velocity.x) > 0)
        {
            anime.SetBool("IsWalking", true);
        }
        else
        {
            anime.SetBool("IsWalking", false);
        }

        if (gameObject.name != "Cleaner" && gameObject.name != "Cleaner (1)" && gameObject.name != "Cleaner (2)")
        {
            if (enemyController.state == EnemyController.ENEMY_STATE.ES_DIE)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                col.enabled = false;
                anime.SetBool("isDead", true);
            }
        }
    }
}
