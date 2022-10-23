using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//총의 회전 피봇에 대한 스크립트
public class GunPivot : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (transform.parent.GetComponent<PlayerController>().StageClear == false)
        {
            if (UIManager.instance.pause < 0)
            {
                // 업데이트 다한 후에 총구 방향 업데이트
                LookDir();
            }
        }
    }

    private void LookDir()//총구가 마우스위치를 바라보게함
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (angle < -90 || angle > 90)//마우스 와 캐릭터위치의 각도가 90도 초과이거나 -90도 미만일때
        {
            transform.localRotation = Quaternion.Euler(180, 0, -angle);
        }
    }
}
