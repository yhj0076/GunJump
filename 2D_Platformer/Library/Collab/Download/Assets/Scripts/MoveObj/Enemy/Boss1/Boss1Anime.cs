using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Anime : MonoBehaviour
{
    Animator anime;
    Boss1Controller controller;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        controller = GetComponent<Boss1Controller>();
    }

    void Update()
    {
        IsGrab();
        IsLaunch();
        IsDead();
    }

    void IsDead()
    {
        if (controller.state == Boss1Controller.BOSS1_STATE.ES_DEAD)
        {
            toIdle();
        }
    }

    void IsGrab()
    {
        if(controller.state == Boss1Controller.BOSS1_STATE.ES_GRAB)
        {
            anime.SetBool("Grab", true);

            Invoke("toIdle",1.2f);
            // if(anime.GetCurrentAnimatorStateInfo(0).IsName("Grab"))
            // {
            //     anime.SetBool("Grab", false);
            // }
        }
    }

    void IsLaunch()
    {
        if(controller.state == Boss1Controller.BOSS1_STATE.ES_LAUNCHED)
        {
            anime.SetBool("Launch", true);
            //if (anime.GetCurrentAnimatorStateInfo(0).IsName("Boss1_AfterLaunch"))
            //{
            //    anime.SetBool("Launch", false);
            //}
            transform.GetChild(1).GetComponent<Animator>().SetBool("Launching", true);
            transform.GetChild(2).GetComponent<Animator>().SetBool("Launching", true);
            Invoke("toIdle", 1.5f);
        }
    }

    void toIdle()
    {
        anime.SetBool("Grab", false);
        anime.SetBool("Launch", false);
        transform.GetChild(1).GetComponent<Animator>().SetBool("Launching", false);
        transform.GetChild(2).GetComponent<Animator>().SetBool("Launching", false);
        //controller.state = Boss1Controller.BOSS1_STATE.ES_IDLE;
    }
}
