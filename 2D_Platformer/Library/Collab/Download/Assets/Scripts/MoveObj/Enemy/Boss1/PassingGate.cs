using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingGate : MonoBehaviour
{
    public bool isInside = false;
    private bool On = true;
    BGMplayer bgm;

    AudioSource MusicPlayer;

    private void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<BGMplayer>();
        MusicPlayer = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"&&On)
        {
            isInside = true;
            On = false;
            GameObject.Find("Player").GetComponent<PlayerController>().DontMove = true;
            Invoke("freezeOff", 5.0f);
            GameObject.Find("Main Camera").GetComponent<MainCamFollow>().StopCam = true;
            GameObject.Find("BackBackGround").GetComponent<MainCamFollow>().StopCam = true;
            GameObject.Find("ForeGround").GetComponent<BackGroundMoving>().Freeze = true;
            GameObject.Find("MidGround").GetComponent<BackGroundMoving>().Freeze = true;
            Invoke("delay", 6.2f);
            GameObject.Find("Main Camera").GetComponent<MainCamFollow>().ShakeCam = true;
            GameObject.Find("Tilemap for BackGround").GetComponent<GridMover>().Invoke("Move", 6f);
            bgm.pauseMusic();
            MusicPlayer.Play();
            MusicPlayer.loop = true;
        }
    }

    void freezeOff()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().DontMove = false;
        MusicPlayer.Stop();
        Invoke("MovingAnimeOn", 1f);
    }

    void delay()
    {
        GameObject.Find("Boundaries").GetComponent<BarrierOn>().BattleStart = true;
        GameObject.Find("Obstacle Maker").GetComponent<ObstacleMover>().makingStart = true;
        bgm.NextTrack();       
    }

    void MovingAnimeOn()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().inBoss1Stage = true;
    }
}
