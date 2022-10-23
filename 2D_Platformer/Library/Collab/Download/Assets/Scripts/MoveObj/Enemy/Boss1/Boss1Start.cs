using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Start : MonoBehaviour
{
    public GameObject StopPos;
    public float BoostSp;
    public bool Stop;

    private void Start()
    {
        Stop = false;
    }
    void Update()
    {
        if (GameObject.Find("Passing Gate").GetComponent<PassingGate>().isInside&&Stop==false)
        {
            Invoke("BattleStart", 6f);
        }
    }

    void BattleStart()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        BossMove();
        //Invoke("BossStart", 1.0f);
        BossStart();
    }

    void BossMove()
    {
        transform.GetChild(0).position = Vector2.MoveTowards(transform.GetChild(0).position, StopPos.transform.position, BoostSp * Time.deltaTime);
    }

    void BossStart()
    {
        GameObject.Find("Boss1").GetComponent<Boss1Controller>().state = Boss1Controller.BOSS1_STATE.ES_IDLE;
    }

}
