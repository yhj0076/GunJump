using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.06
 * 최종 수정자 : 이호준
 * 파일 이름 : BackGroundMoving.cs
 * 클래스 내용 : 배경에 입체감을 심어주는 클래스
 */

public class BackGroundMoving : MonoBehaviour
{
    GameObject Player;   // 플레이어를 가져온다.

    public float Sensitivity;   // 감도. 숫자가 높을 수록 안움직인다. 0이면 정지.
    public bool Freeze = false;

    private void Awake()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 y값은 그대로 가져오지만, x값은 나눈다.
        //this.transform.position = new Vector2(Player.transform.position.x / Sensitivity, Player.transform.position.y-4);

        Moving();
    }

    void Moving()
    {
        if(Freeze)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        else
        {
            if (Player.transform.position.y < -15)
            {
                transform.position = new Vector2(Player.transform.position.x / Sensitivity, -21);
            }
            else
            {
                transform.position = new Vector2(Player.transform.position.x / Sensitivity, Player.transform.position.y - 6);
            }
        }
    }
}

