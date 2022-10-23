using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCamBoundary : MonoBehaviour
{
    public GameObject cam;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cam.GetComponent<MainCamFollow_Boss1>().StopCam = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.GetComponent<MainCamFollow_Boss1>().StopCam = false;
        }
    }
}
