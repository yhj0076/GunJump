using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Fade : MonoBehaviour
{
    Image image;
    public float fade;

    float time = 0;

    Color color;
    public bool Fading;    // true : Fade In, false : Fade Out
    private void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
        switch (SceneManager.GetActiveScene().name)
        {
            case "stage 1":      // 페이드 인으로 시작
            case "stage 2":
            case "RecentBossStage1":
                color.a = 1;
                time = 0;
                Fading = false;
                break;
            default:            // 걍 시작
                color.a = 0;
                time = 0;
                Fading = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {      
        switch (Fading)
        {
            case true:      // Fade In
                if (color.a >= 0&&color.a < 1)
                {
                    time += Time.deltaTime*Mathf.PI/2*fade;
                    color.a = Mathf.Sin(time);
                }
                
                if(time > Mathf.PI/2)
                {
                    time = 0;
                    color.a = 1;
                }
                break;
            case false:     // Fade Out
                if (color.a <= 1&&color.a > 0)
                {
                    time += Time.deltaTime * Mathf.PI / 2 * fade;
                    color.a = Mathf.Cos(time);
                }

                if(time>Mathf.PI/2)
                {
                    time = 0;
                    color.a = 0;
                }
                break;
        }
        
        image.color = color;
    }
}
