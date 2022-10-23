using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 새로운 총 알고리즘 도입(레이캐스트 기준)
// 중간에 주석처리되어있는 코드는 효과음 삽입시 해제요망

//12.25 현민 수정사항 : 맨손파이트
public class NoHand : MonoBehaviour
{
    PlayerController playerController;

    public enum State
    {
        Ready,      // 발사 가능
        Empty,      // 탄알 부족
        Reloading   // 재장전중
    }

    public State state
    {
        get;
        private set;
    }

    public GameObject firePoint;            // 탄알발사 위치게임오브젝트
    private Vector2 fireTransform;          // 탄알의 발사 위치
    private LineRenderer bulletLine;        // 총의 궤적
    
    
    // Start is called before the first frame update
    void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();  // 플레이어무브 컴포넌트 가져오기

        bulletLine.positionCount = 2;
        bulletLine.enabled = false;

        state = State.Ready;    // 발사 준비 태세
    }

    private void Update()
    {
        fireTransform = firePoint.transform.position;
        playerController.GetPlayerInfo();
    }
}
