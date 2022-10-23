using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.06
 * 최종 수정자 : 정현민
 * 최종 수정일 : 2021.03.14
 * 수정 내용 : 미니맵에 배경을 없애기위해 3차원벡터로 바꾸고 z값 조정을함 39,45번째줄 
 * 파일 이름 : BackGroundMainScroller.cs
 * 클래스 내용 : 배경이 이어지게 보이도록 해주는 클래스
 */

public class BackGroundMainScroller : MonoBehaviour
{
    GameObject Player;   // 플레이어를 가져온다.

    private float width;    // 배경의 길이값

    void Awake()
    {
        Player = GameObject.Find("Player");
        BoxCollider2D BG = GetComponent<BoxCollider2D>();
        width = BG.size.x;  // BoxCollider2D를 이용해 배경의 길이를 구한다.
    }

    // Update is called once per frame
    void LateUpdate()
    {
         Replace();  // 위치 배치 함수
    }

    void Replace()
    {
        if (Player.transform.position.y > -15)
        {
            // 플레이어가 다른 배경 중앙에서 오른쪽으로 갈 때
            if (Player.transform.position.x > transform.position.x + width)
            {
                // 배경을 오른쪽으로 넘긴다.
                transform.position = new Vector3(transform.position.x + width * 2f, transform.position.y, -14.0f);
            }
            // 플레이어가 다른 배경 중앙에서 왼쪽으로 갈 떄
            else if (Player.transform.position.x < transform.position.x - width)
            {
                // 배경을 왼쪽으로 넘긴다.
                transform.position = new Vector3(transform.position.x - width * 2f, transform.position.y, -14.0f);
            }
        }
        else
        {
            // 플레이어가 다른 배경 중앙에서 오른쪽으로 갈 때
            if (Player.transform.position.x > transform.position.x + width)
            {
                // 배경을 오른쪽으로 넘긴다.
                transform.position = new Vector3(transform.position.x + width * 2f, -15, -14.0f);
            }
            // 플레이어가 다른 배경 중앙에서 왼쪽으로 갈 떄
            else if (Player.transform.position.x < transform.position.x - width)
            {
                // 배경을 왼쪽으로 넘긴다.
                transform.position = new Vector3(transform.position.x - width * 2f, -15, -14.0f);
            }
        }
    }
}
