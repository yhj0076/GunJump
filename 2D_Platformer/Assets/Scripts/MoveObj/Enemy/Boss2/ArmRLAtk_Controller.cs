using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRLAtk_Controller : MonoBehaviour
{
    public int RL;  // R = 1, L = -1
    public bool DoIt;

    private void Start()
    {
        DoIt = false;
    }

    private void Update()
    {
        if(DoIt)
        {
            transform.GetChild(0).localPosition = new Vector2(RL * Random.Range(4, 6), 0);
            transform.GetChild(1).localPosition = new Vector2(RL * Random.Range(6, 8), 0);
            transform.GetChild(2).localPosition = new Vector2(RL * Random.Range(8, 10), 0);
            Invoke("delayOn1", 0.0f);
            Invoke("delayOn2", 0.3f);
            Invoke("delayOn3", 0.6f);
            DoIt = false;
        }
    }

    void delayOn1()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("isAttack", true);
    }
    void delayOn2()
    {
        transform.GetChild(1).GetComponent<Animator>().SetBool("isAttack", true);
    }
    void delayOn3()
    {
        transform.GetChild(2).GetComponent<Animator>().SetBool("isAttack", true);
    }
}