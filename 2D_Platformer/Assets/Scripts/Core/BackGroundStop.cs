using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundStop : MonoBehaviour
{
    void Update()
    {
        if(GameObject.Find("Main Camera").GetComponent<MainCamFollow>().StopCam)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
