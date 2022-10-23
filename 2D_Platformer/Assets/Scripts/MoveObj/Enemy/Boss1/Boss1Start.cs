using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Start : MonoBehaviour
{
    public GameObject cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.GetComponent<MainCamFollow_Boss1>().StopCam = true;
            cam.GetComponent<MainCamFollow_Boss1>().ShakeCam = true;
            Invoke("Boss1_Start", 4);
        }
    }

    void Boss1_Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        FindObjectOfType<GridMover>().StageStart = true;
        Destroy(this);
    }
}
