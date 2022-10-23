using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone_Stage3_1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damaged(100);
        }
    }
}
