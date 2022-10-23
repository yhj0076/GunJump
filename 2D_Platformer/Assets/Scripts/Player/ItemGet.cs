using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.27
 * 최종 수정자 : 이호준
 * 최종 수정일 : 2021.05.30
 * 수정 내용 : 아이템의 개수가 늘어나면 바로 적용할 수 있도록 했다. 우리는 else if만 더 추가하면 된다. 단, 플레이어 인덱스순서에 꼭 맞춰야 한다.
 * 파일 이름 : ItemGet.cs
 * 클래스 내용 : 아이템 획득
 */

public class ItemGet : MonoBehaviour
{
    int index = 0;

    int lastIndex = 4;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int i = 0;

            while (i < lastIndex)
            {
                collision.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
                i++;
            }
        
            //switch는 문자열을 판별할 수 없다. 따라서 if를 통해 찾는다.
            if (this.gameObject.name == "Gun_Mk.2")
            {
                index = 1;
            }
            else if (this.gameObject.name == "SF_Gun1")
            {
                index = 2;
            }
            else if (this.gameObject.name == "Main_Gun")
            {
                index = 3;
            }


        
            collision.transform.GetChild(0).GetChild(index).gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
