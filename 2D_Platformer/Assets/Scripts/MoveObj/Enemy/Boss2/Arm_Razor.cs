using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_Razor : MonoBehaviour
{
    float time = 0;
    public float speed;
    public float waitTime;

    private void OnEnable()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime*speed;
        if(time < waitTime)
        {
            transform.rotation = Quaternion.Euler(0,0,-50+time);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        time = 0;
        transform.rotation = Quaternion.Euler(0, 0, -50);
    }
}
