using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : MonoBehaviour
{
    public GameObject scanObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            scanObject = collision.gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            scanObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        scanObject = null;
    }
}
