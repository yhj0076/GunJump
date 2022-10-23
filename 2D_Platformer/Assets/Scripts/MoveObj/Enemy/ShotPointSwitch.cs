using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.15
 * 최종 수정자 : 이호준
 * 파일 이름 : ShotPointSwith.cs
 * 클래스 내용 : Enemy의 Child의 x값을 -x값으로 바꾸는 스크립트
 */
public class ShotPointSwitch : MonoBehaviour
{
    Vector2 vec;
    Vector2 sc;
    
    void Awake()
    {
        vec = this.transform.localPosition;
        sc = this.transform.localScale;
    }

    private void Update()
    {
        if(GetComponentInParent<EnemyController>().isSplit)
        {
            this.transform.localPosition = new Vector2(-vec.x, vec.y);
            this.transform.localScale = new Vector2(-sc.x, sc.y);
        }
        else
        {
            this.transform.localPosition = new Vector2(vec.x, vec.y);
            this.transform.localScale = new Vector2(sc.x, sc.y);
        }
    }
}
