using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 새로운 총 알고리즘 도입(레이캐스트 기준)
// 중간에 주석처리되어있는 코드는 효과음 삽입시 해제요망

//12.25 현민 수정사항 : 총알발사관련해서 플레이어 위치 변수대신 총구의 위치로 넣음 수정사항 코드 줄에 날짜 표기함.
//21.04.08 호준 수정사항 : 총구 방향 수정 및 오토기능 추가
//21.05.15 현민 : 총쏴서 벽 부수는거 추가
public class Gun : MonoBehaviour
{
    PlayerController playerController;
    Animator anime;

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

    private AudioSource gunAudioPlayer;
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public bool FullAuto;
    public int damage = 25;                 // 공격력
    public float MaxDistance = 50f;         // 사정거리
    public float recoil = 10f;              // 반동 힘
    public float BackOff = 5f;              // 피격대상 관성

    public int MaxAmmo = 6;                 // 최대 총알 수
    public int NowAmmo;                     // 현재 총알 수

    public float FireRate = 0.5f;           // 발사 속도
    public float reloadTime = 1.0f;         // 재장전 시간
    private float lastFireTime = 0;         // 마지막 발사 시점

  
    
    // Start is called before the first frame update
    void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLine = GetComponent<LineRenderer>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();  // 플레이어무브 컴포넌트 가져오기
        anime = GameObject.Find("Player").GetComponent<Animator>();

        bulletLine.positionCount = 2;
        bulletLine.enabled = false;

        NowAmmo = MaxAmmo;      // 탄 가득 채우기
        state = State.Ready;    // 발사 준비 태세
        lastFireTime = 0;       // 발사 시점 초기화
    }

    private void Start()
    {
        UIManager.instance.AmmoCount(NowAmmo);
    }

    private void Update()
    {
        fireTransform = firePoint.transform.position;
        playerController.GetPlayerInfo();
        /* 총알 입력 메서드 */
        if (Input.GetKey(KeyCode.R) || state == State.Empty)    // R키 혹은 총알이 다 떨어졌을 시 재장전
        {
            Reload();
        }
        if (FullAuto)
        {
            if (Input.GetMouseButton(0) && UIManager.instance.pause < 0 && UIManager.instance.Dead == false)
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && UIManager.instance.pause < 0 && UIManager.instance.Dead == false)    // 마우스 좌클릭으로 발사
            {
                Fire();
            }
        }      
    }

    // 쏜다? 어어? 쏜다?
    public void Fire()
    {
        if(state == State.Ready && Time.time >= lastFireTime + FireRate)
        {
            Shot();
            lastFireTime = Time.time;
        }
    }
    
    // 빵!
    private void Shot()
    {
        RaycastHit2D hit ;
        Vector2 hitPosition = Vector2.zero;
        Vector2 ShotStart = new Vector2(firePoint.transform.position.x,   //firePoint가 총구
                                        firePoint.transform.position.y);

        if (hit = Physics2D.Raycast(ShotStart, playerController.MousePosition, MaxDistance))  //총을 쏴서 맞으면
        {

            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.tag == "Enemy")
            {
                Vector2 backOff = new Vector2(playerController.MousePosition.normalized.x*BackOff, playerController.MousePosition.normalized.y*BackOff);
                if (hit.collider.name == "Boss1")
                {
                    hit.collider.gameObject.GetComponent<Boss1_HealthControl>().Damaged(damage);
                }
                else if(hit.collider.name == "Hydey")
                {
                    hit.collider.gameObject.GetComponent<Boss2_Health>().Damaged(damage);
                }
                else if(hit.collider.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<Enemy>().Damaged(damage);
                }
                else
                {

                }
                switch (hit.collider.name)
                {
                    case "Ripper":
                    case "Boss1":
                    case "Hydey":
                        break;
                    default:
                        hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = backOff;
                        break;
                }   
            }

            //쏴서 맞은거가 Breakable Walls 태그를 달고 있을 시
            if (hit.collider.tag == "Breakable Walls")
            {
                //tileMap 변수에 맞은거의 Tilemap 컴포넌트를 가져옴
                Tilemap tileMap = hit.collider.gameObject.GetComponentInChildren<Tilemap>();

                Vector3Int cellPosition = tileMap.WorldToCell(hit.point);

                Debug.Log(cellPosition);
                if (tileMap.GetTile(cellPosition) != null)
                {
                    tileMap.SetTile(cellPosition, null);
                    hit.collider.gameObject.GetComponent<AfterBreakableWall>().BreakEffect(cellPosition);
                }
            }

            hitPosition = hit.point;
        }
        else      // 안맞으면
        {
            hitPosition = (playerController.MousePosition.normalized * MaxDistance) + fireTransform;//이부분 수정(12.25)
        }

        StartCoroutine(ShotEffect(hitPosition));

        NowAmmo--;
        UIManager.instance.AmmoCount(NowAmmo);

        if(NowAmmo<=0)
        {
            state = State.Empty;
        }

        playerController.recoil(recoil);
    }

    // 라인렌더러를 이용한 총알 발사 효과
    private IEnumerator ShotEffect(Vector2 hitPosition)
    {
        gunAudioPlayer.PlayOneShot(shotClip);

        bulletLine.SetPosition(0, fireTransform);//이부분수정 (12.25)
        bulletLine.SetPosition(1, hitPosition);
        bulletLine.enabled = true;

        yield return new WaitForSeconds(0.03f);
        bulletLine.enabled = false;
    }

    // 재장전
    public bool Reload()
    {
        if(state == State.Reloading || NowAmmo<0)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 재장전 루틴
    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        UIManager.instance.Reload();
        gunAudioPlayer.PlayOneShot(reloadClip);

        yield return new WaitForSeconds(reloadTime);

        NowAmmo = MaxAmmo;

        state = State.Ready;
        UIManager.instance.ReloadEnd();
        UIManager.instance.AmmoCount(NowAmmo);
    }
}
