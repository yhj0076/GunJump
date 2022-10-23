using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour    // 함정 발동 스크립트(02.05)
{
    ElectricTrap trap1;

    private void Awake()
    {
        trap1 = GameObject.FindGameObjectWithTag("Trap").GetComponent<ElectricTrap>();
    }

    // 건들기만 하면 바로 작동
    private void OnCollisionEnter2D(Collision2D collision)
    {
        trap1.TrapState = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        trap1.TrapState = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trap1.TrapState = false;
    }
}
