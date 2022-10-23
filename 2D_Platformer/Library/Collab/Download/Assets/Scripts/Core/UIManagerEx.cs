using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerEx : MonoBehaviour
{
    public static UIManager instance;
    public Text AmmoCounting;
    public Image healthBar;
    public GameObject Reloading;
    public Canvas GameOver;
    public GameObject Minimap;
    GameObject ESC;
    GameObject Player;

    public bool Dead = false;
    public int pause = -1;     // 1일때 ture, -1일때 false

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ESC = GameObject.Find("ESC");
            Player = GameObject.Find("Player");
            PlayerPrefs.SetFloat("timeScale", 1);
        }
        else
        {
            Debug.LogError("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pause = pause * -1;
        }

        if (Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            Time.timeScale = 0;
            Dead = true;
        }
        else
        {
            if (pause > 0)
            {
                Time.timeScale = 0;
                ESC.SetActive(true);
            }
            else
            {
                Time.timeScale = PlayerPrefs.GetFloat("timeScale");
                ESC.SetActive(false);
            }
        }
    }

    public void AmmoCount(int NowBullet) // 남은 총알 수 세기
    {
        AmmoCounting.text = ""+NowBullet;
    }

    public void Health(int HP)  // 플레이어의 체력
    {
        healthBar.fillAmount = HP / 100f;
    }

    public void Reload()    // 장전중
    {
        Reloading.SetActive(true);
    }

    public void ReloadEnd() // 장전 완료
    {
        Reloading.SetActive(false);
    }

    public void GameOverUI()   //게임오버 캔버스 띄우기
    {
        GameOver.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnMinimap()
    {
        Minimap.SetActive(true);
    }

    public void OffMinimap()
    {
        Minimap.SetActive(false);
    }
}
