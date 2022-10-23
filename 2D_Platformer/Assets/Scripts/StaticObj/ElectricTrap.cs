using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : MonoBehaviour   // 전기 함정 작동 스크립트(02.05)
{
    public GameObject StartRazor;
    public GameObject EndRazor;
    private LineRenderer Razor;
    /*
    private AudioSource TrapAudioPlayer;
    public AudioClip TrapOnSound;
    */
    public bool TrapState = false;  // 함정의 상태 T = 활성화, F = 비활성화

    public int Damage;              // 함정의 데미지
    public float JumpWhenDamaged;   // 피격시 점프 강도

    void Start()
    {
        Razor = GetComponent<LineRenderer>();
        /*TrapAudioPlayer = GetComponent<AudioSource>();*/  // 오디오 설정
    }

    void FixedUpdate()
    {
        if(TrapState == true)   // 트랩 작동 시
        {
            RaycastHit2D hit;
            Vector2 StartRazorP = new Vector2(StartRazor.transform.position.x + 0.5f, StartRazor.transform.position.y);
            Vector2 EndRazorP = new Vector2(EndRazor.transform.position.x - 0.5f, StartRazor.transform.position.y);
            if (hit = Physics2D.Raycast(StartRazorP,EndRazorP))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().Damaged(Damage);
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity.x,
                                                                                               hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity.y + JumpWhenDamaged);
                }
            }

            Razor.SetPosition(0, StartRazorP);
            Razor.SetPosition(1, EndRazorP);
            Razor.enabled = true;
            /*TrapAudioPlayer.PlayOneShot(TrapOnSound);*/   // 오디오 설정
        }
        else
        {
            Razor.enabled = false;
        }
    }
}
