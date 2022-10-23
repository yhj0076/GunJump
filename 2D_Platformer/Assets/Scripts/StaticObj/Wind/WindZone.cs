using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour   // 2021.01.27
{
    AudioSource soundPlayer;
    public Vector2 WindPower;   // 날아가는 바람 힘

    private void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)  
    {
        if (collision.gameObject.tag == "Player")   // 안에 "Player"태그가 달린 오브젝트가 있다면
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(WindPower);   // WindPower방향으로 민다.
            soundPlayer.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        soundPlayer.Pause();
    }

}
