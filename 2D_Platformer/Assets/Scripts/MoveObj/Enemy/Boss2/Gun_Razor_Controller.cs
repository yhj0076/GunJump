using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Razor_Controller : MonoBehaviour
{
    private void OnEnable()
    {
        if(transform.parent.GetComponent<SpriteRenderer>().flipX)
        {
            transform.localRotation = Quaternion.Euler(0,0,180);
            transform.localPosition = new Vector2(-0.89f, -0.63f);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0,0,0);
            transform.localPosition = new Vector2(0.89f, -0.63f);
        }
        Invoke("disableObj",0.03f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().corZonya();
        }
    }

    void disableObj()
    {
        gameObject.SetActive(false);
    }
}
