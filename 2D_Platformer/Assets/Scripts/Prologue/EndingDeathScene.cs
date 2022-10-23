using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndingDeathScene : MonoBehaviour
{
    public Image image;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("FadeOut",0.5f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        
    }

    void FadeOut()
    {
        image.GetComponent<Fade>().Fading = true;
        image.GetComponent<Fade>().fade = 0.2f;
    }

    private void Update()
    {
        if(image.color.a == 1)
        {
            GameManager.instance.NextGame();
        }
    }
}
