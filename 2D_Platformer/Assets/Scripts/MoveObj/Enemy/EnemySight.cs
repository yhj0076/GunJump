using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.15
 * 최종 수정자 : 이호준
 * 파일 이름 : EnemySight.cs
 * 클래스 내용 : 적의 시야를 FlipX하는 스크립트
 */

public class EnemySight : MonoBehaviour
{
    Vector3 scale;
    void Update()
    {
        scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        if (GetComponentInParent<EnemyController>().isSplit)
        {
            transform.localScale = -scale;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<EnemyController>().state = EnemyController.ENEMY_STATE.ES_ATTACK;
            GetComponentInParent<EnemyAttack>().isFire = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInParent<EnemyController>().state = EnemyController.ENEMY_STATE.ES_IDLE;
        GetComponentInParent<EnemyAttack>().isFire = false;
    }
}
