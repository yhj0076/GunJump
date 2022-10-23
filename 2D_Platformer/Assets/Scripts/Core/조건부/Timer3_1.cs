using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer3_1 : MonoBehaviour
{
    public Text Timer;
    public int Timer_Minute;
    public int Timer_Second;
    string timer_min;
    string timer_sec;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        Timer.text = Timer_Minute + ":"+Timer_Second;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>=1)
        {
            if (Timer_Second > 0)
            {
                Timer_Second--;
                time = 0;
            }
            else
            {
                if (Timer_Minute > 0)
                {
                    Timer_Minute--;
                    Timer_Second = 59;
                    time = 0;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
        MinWithZero();
        SecWithZero();
        Timer.text = timer_min + ":" + timer_sec;
    }

    private void OnDisable()
    {
        FindObjectOfType<Fade>().Fading = true;
    }

    void MinWithZero()
    {
        if (Timer_Minute < 10)
        {
            timer_min = "0" + Timer_Minute;
        }
        else
        {
            timer_min = "" + Timer_Minute;
        }
    }
    void SecWithZero()
    {
        if (Timer_Second < 10)
        {
            timer_sec = "0" + Timer_Second;
        }
        else
        {
            timer_sec = "" + Timer_Second;
        }
    }
}
