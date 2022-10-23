using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamFollowInBoss2 : MonoBehaviour
{
    public bool StopCam;
    public bool ShakeCam;
    public float ShakeTime;
    public float ShakePower;

    Vector3 originalP;

    GameObject Player;
    PlayerController playerController;

    float middleY;
    private void Awake()
    {
        Player = GameObject.Find("Player");
        middleY = Player.transform.position.y;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (StopCam != true)
        {
            if (Player == null)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                if (Player.transform.position.y < -15)
                {
                    transform.position = new Vector3(Player.transform.position.x, -14, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(Player.transform.position.x,-0.5f, transform.position.z);
                }
            }
            originalP = transform.position;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if (ShakeCam)
        {
            Shake(originalP, ShakeTime);
        }
    }

    void Shake(Vector3 originalP, float time)
    {
        if (time > 0)
        {
            transform.position = originalP + new Vector3(0, Random.Range(ShakePower * -1, 0), 0);
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = originalP;
            ShakeCam = false;
        }
    }
}
