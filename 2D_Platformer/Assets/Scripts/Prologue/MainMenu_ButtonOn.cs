using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu_ButtonOn : MonoBehaviour
{
    public GameObject START_B;
    public GameObject STAGE_B;
    public GameObject EXIT_B;

    public GameObject MoveObj;
    public GameObject Fade;

    public Image TitleWithButtonScreen1;
    public Image TitleWithButtonScreen2;
    public float FadeOutSpeed;
    bool titleWithButtonScreen = false;
    float time = 0;

    Animator anime;

    private void Awake()
    {
        ButtonDisable();
        anime = GetComponent<Animator>();
        titleWithButtonScreen = false;

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("Main_Idle"))
        {
            ButtonAble();
        }

        if(titleWithButtonScreen && time<Mathf.PI/2)
        {
            time += Time.deltaTime*FadeOutSpeed;
            TitleWithButtonScreen1.color = new Color(1,1,1,Mathf.Cos(time));
            TitleWithButtonScreen2.color = new Color(1,1,1,Mathf.Cos(time));
        }
    }

    void ButtonDisable()
    {
        START_B.SetActive(false);
        STAGE_B.SetActive(false);
        EXIT_B.SetActive(false);
    }    // 버튼 비활성화

    void ButtonAble()
    {
        START_B.SetActive(true);
        STAGE_B.SetActive(true);
        EXIT_B.SetActive(true);
    }       // 버튼 활성화

    public void StartAnimation()
    {
        ButtonDisable();
        titleWithButtonScreen = true;
        MoveObj.SetActive(true);
        Invoke("FadeOut",6.7f);
        Invoke("GotoTutorial",10.5f);
    }

    void GotoTutorial()
    {
        GameManager.instance.NextGame();
    }

    void FadeOut()
    {
        Fade.GetComponent<Fade>().Fading = true;
    }
}
