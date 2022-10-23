using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_1Turret : MonoBehaviour
{
    public enum TR3_1STATE
    {
        TR_AIMING,
        TR_SHOT
    }

    public TR3_1STATE state
    {
        get;
        set;
    }

    GameObject Player;
    float time = 0;
    bool shoot = false;
    public float shotTime;
    public int dmg;
    LineRenderer razor;
    AudioSource soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
        time = 0;
        state = TR3_1STATE.TR_AIMING;
        razor = GetComponent<LineRenderer>();
        razor.positionCount = 2;
        razor.enabled = false;
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(state == TR3_1STATE.TR_SHOT)
        {
            if(time>1f)
            {
                state = TR3_1STATE.TR_AIMING;
                time = 0;
            }
        }
        else
        {
            if(time>shotTime)
            {
                state = TR3_1STATE.TR_SHOT;
                time = 0;
            }
        }

        switch (state)
        {
            case TR3_1STATE.TR_AIMING:
                Aiming();
                break;
            case TR3_1STATE.TR_SHOT:
                if (shoot == false)
                {
                    Invoke("Shot",0.3f);
                    shoot = true;
                }
                break;
        }
    }

    void Aiming()
    {
        Vector2 dir = transform.position - Player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        shoot = false;
    }

    void Shot()
    {
        RaycastHit2D hit;
        Vector2 hitPosition = Vector2.zero;

        if (hit = Physics2D.Raycast(transform.position,transform.GetChild(0).position-transform.position, Mathf.Infinity))  //총을 쏴서 맞으면
        {
            Debug.Log("맞았다");
            if (hit.collider.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerController>().Damaged(dmg);
                if(transform.parent.localScale.x > 0)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-7,7);
                }
                else
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(7,7);
                }
            }
            hitPosition = hit.point;
        }
        StartCoroutine(ShotEffect(hitPosition));
    }

    private IEnumerator ShotEffect(Vector2 hitPosition)
    {
        soundPlayer.Play();

        razor.SetPosition(0, transform.GetChild(0).transform.position);//이부분수정 (12.25)
        razor.SetPosition(1, hitPosition);
        razor.enabled = true;

        yield return new WaitForSeconds(0.03f);
        razor.enabled = false;
        StopCoroutine("ShotEffect");
    }
}