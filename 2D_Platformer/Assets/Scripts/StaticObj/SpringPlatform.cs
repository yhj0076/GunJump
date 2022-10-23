using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    Animator anime;
    AudioSource soundPlayer;
    public AudioClip Up;
    public AudioClip Down;

    public float pushPower;
    public int dirIndex;

    Vector2 POW;

    bool Downing = false;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        soundPlayer = GetComponent<AudioSource>();
        switch (dirIndex)
        {
            case 0:
                POW = Vector2.right;
                break;
            case 1:
                POW = Vector2.left;
                break;
            case 2:
                POW = Vector2.down;
                break;
            case 3:
                POW = Vector2.up;
                break;
            default:
                POW = Vector2.zero;
                Debug.LogError("스프링 방향 숫자 오류!");
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("Spring_Down") || anime.GetCurrentAnimatorStateInfo(0).IsName("SpringLab_Down"))
        {
            anime.SetBool("isPush",false);
            if(Downing)
            {
                if (Down != null)
                {
                    soundPlayer.PlayOneShot(Down);
                }
                Downing = false;
            }
        }
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anime.SetBool("isPush",true);
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("Spring_Down")==false|| anime.GetCurrentAnimatorStateInfo(0).IsName("SpringLab_Down") == false)
        {
            soundPlayer.PlayOneShot(Up);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(POW * pushPower, ForceMode2D.Impulse);
            Downing = true;
        }
    }
}
