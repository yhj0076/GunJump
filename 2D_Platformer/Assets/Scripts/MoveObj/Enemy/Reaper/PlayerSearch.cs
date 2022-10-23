using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    Reaper reaper;
    // Start is called before the first frame update
    void Start()
    {
        reaper = GetComponent<Reaper>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            reaper.Searched = true;
            if (transform.position.x<collision.gameObject.transform.position.x)
            {
                reaper.frontCheck = 1;
            }
            else if(transform.position.x > collision.gameObject.transform.position.x)
            {
                reaper.frontCheck = -1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            reaper.Searched = false;
        }
    }
}
