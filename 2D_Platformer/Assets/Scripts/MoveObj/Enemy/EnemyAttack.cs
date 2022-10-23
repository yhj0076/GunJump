using System.Collections;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.15
 * 최종 수정자 : 이호준
 * 파일 이름 : ObjectRain.cs
 * 클래스 내용 : Enemy2의 손에서 총이 나오게 하는 스크립트
 */

/*
 21.05.14 현민 수정
 -주석 좀 추가
 -양 손에서 총을쏘게 수정
*/

/*
 21.05.15 호준 수정
 -주석 좀 추가
 -발사 및 FlipX 상태 확인 수정
*/
public class EnemyAttack : MonoBehaviour
{
    //플레이어가 직접 넣을수있는 총구위치값
    public GameObject firePoint1;
    public GameObject firePoint2;
    //쏠 수 있느 한계거리
    public GameObject ShotLimit;
    //한계거리에서 한단계 처리가 된 변수
    private Vector2 ShotLimitPoint; 
    //총구위치값에서 한단계 처리가 된 변수
    private Vector2 rightFirePoint;
    private Vector2 leftFirePoint;
    
    private LineRenderer bulletLine;
    PlayerController playerController;
    EnemyController enemyController;
    private bool isRighthand = true;    //오른손 차례인가
    public bool isFire = false;         //발사 명령


    private AudioSource gunAudioPlayer;
    public AudioClip shotClip;

    public bool TurretMode;
    public int damage;
    public float MaxDistance;
    public float BackOff;
    public float FireRate = 0.5f;
    public float lastFireTime = 0;



    void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLine = GetComponent<LineRenderer>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyController = GetComponent<EnemyController>();
        
        bulletLine.positionCount = 2;
        bulletLine.enabled = false;
    }

    void Update()
    {
        rightFirePoint = firePoint1.transform.position;
        leftFirePoint = firePoint2.transform.position;
        if(isFire)
        {
            Fire();
        }
    }
    
    public void Fire()
    {
        if(Time.time>= lastFireTime + FireRate)
        {
            Shot();
            lastFireTime = Time.time;
        }
    }
    
    private void Shot()
    {
        
        RaycastHit2D hit;
        Vector2 hitPosition = Vector2.zero;
        //총구 위치
        Vector2 ShotStart = new Vector2(firePoint1.transform.position.x,
                                        firePoint1.transform.position.y);
        //총알이 나가는 한계점 사정거리
        ShotLimitPoint = ShotLimit.transform.position;

        //거리 계산
        Vector2 offset = ShotLimitPoint - ShotStart;
        //거리 벡터의 방향값 계산
        Vector2 ShotEnd = offset.normalized;

       //레이어마스크 설정으로 플레이어만 맞게 한다
        int layerMask = 1 << LayerMask.NameToLayer("Player");

        //레이를쏜다 플레이어에게만 맞게
        if (hit = Physics2D.Raycast(ShotStart,ShotEnd,MaxDistance,layerMask))
        {
           
           
            //히트한것의 태그가 플레이어일시
            if (hit.collider.tag == "Player")
            {
                Vector2 backOff = new Vector2(GetComponent<EnemyController>().frontCheck*BackOff, 0);

                //플레이어에게 데미지주기
                hit.collider.gameObject.GetComponent<PlayerController>().Damaged(damage);
                hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(backOff);
                
            }
            //히트포지션변수에 레이가맞은곳의 정보를준다.
            hitPosition = new Vector2(hit.point.x,ShotStart.y);
        }
        //아무도 안맞으면
        else
        {
            //히트포지션변수에 샷리밋포인트 의 정보를준다.
            hitPosition = ShotLimitPoint;
        }
        //Debug.DrawRay(ShotStart, ShotEnd, Color.red);

        //히트포지션 정보를주면서 샷이펙트 코루틴시작
        StartCoroutine(ShotEffect(hitPosition));
    }

    //여기서 불릿라인 생성
    private IEnumerator ShotEffect(Vector2 hitPosition)
    {
        //사운드재생
        Vector3 viewPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (Mathf.Abs(viewPoint.x) <= 1 &&
           Mathf.Abs(viewPoint.x) >= 0 &&
           Mathf.Abs(viewPoint.y) <= 1 &&
           Mathf.Abs(viewPoint.y) >= 0 &&
           Mathf.Abs(viewPoint.z) >= 1)
            gunAudioPlayer.PlayOneShot(shotClip);

        //isRighthand변수로 왼쪽 오른쪽 결정
        switch(isRighthand)
        {
            //오른손 차례면
            case true:
                bulletLine.SetPosition(0, rightFirePoint);
                bulletLine.SetPosition(1, hitPosition);
                isRighthand = false;
                break;
                //왼손차례면
            case false:
                bulletLine.SetPosition(0, leftFirePoint);
                bulletLine.SetPosition(1, hitPosition);
                isRighthand = true;
                break;
        }
        
        bulletLine.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLine.enabled = false;
    }
}

