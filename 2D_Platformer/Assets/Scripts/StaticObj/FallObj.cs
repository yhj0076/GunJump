using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.11
 * 최종 수정자 : 이호준
 * 파일 이름 : FallObj.cs
 * 클래스 내용 : 오브젝트 접촉 시 데미지를 주고 튀게 만드는 클래스
 */

public class FallObj : MonoBehaviour
{
    public int damage;
    public int BackOff;

    Rigidbody2D rigid;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)  // 플레이어와 낙하 오브젝트가 충돌했을 시 플레이어에게 데미지
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 backOff = new Vector2(collision.rigidbody.position.normalized.x * BackOff,
                                          collision.rigidbody.position.normalized.y * BackOff);

            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = backOff;    // 충돌시 튕겨나기
            Destroy(this.gameObject, 0);
        }
    }
}
