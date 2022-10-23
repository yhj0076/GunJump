using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int stageNumber = 1;
    private const int MAX_STAGE = 3;
    private Scene scene;

    public bool isAction;                       //조사중인지아닌지
    public GameObject scanObject;   //player 콜라이더에 닿은 것
    
    private void Start()
    {
        stageNumber = SceneManager.GetActiveScene().buildIndex;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }



    public void GameStart()
    {
        SceneManager.LoadScene(stageNumber);
    }

    public void GameRestart()
    {
        Debug.Log(stageNumber);
        SceneManager.LoadScene(stageNumber);
    }

    public void NextGame()
    {
        stageNumber = SceneManager.GetActiveScene().buildIndex;
        if (stageNumber + 1> MAX_STAGE)
        {
            stageNumber = MAX_STAGE;
        }
        else
        {
            ++stageNumber;
        }
        SceneManager.LoadScene(stageNumber);
    }

    public void SelectScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void _ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit(); // 어플리케이션 종료
#endif
    }
    /*public void Action(GameObject scanObj)  //조사,대화
    {

        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.Is_Npc);
        
        talkBar.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)   //대화!
    {
        string talkData = talkManager.GetTalk(id, talkIndex);   //지역변수 talkData 만들어서 값넣음


        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Time.timeScale = 1; //게임활성화
            return;
        }

        if (isNpc)  //엔피씨면
        {
            talkText.text = talkData.Split(':')[0]; // talkData에 들어있는 문장을 :로 구분해서 나눈 배열의 0번째 (대화값)
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));// talkData에 들어있는 문장을 :로 구분해서 나눈 배열의 1번째 (초상화값)
            portraitImg.color = new Color(1, 1, 1, 1);      //엔피씨면 초상화 활성화
        }
        else
        {
            talkText.text = talkData;   //초상화없으니 split안씀
            portraitImg.color = new Color(1, 1, 1, 0);      //엔피씨아니면 초상화 비활성화
        }

        isAction = true;
        Time.timeScale = 0; //게임비활성화
        talkIndex++;
    }*/
}
