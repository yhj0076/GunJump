using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoin : MonoBehaviour
{
    public int power = 10;
    

   
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerController>().Damaged(power);
            Destroy(gameObject, 0);
        }
        else if (col.gameObject.tag == "Platform")
        {
            Destroy(gameObject, 0);
        }
    }

}
