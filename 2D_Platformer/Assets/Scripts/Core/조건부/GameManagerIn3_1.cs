using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagerIn3_1 : MonoBehaviour
{
    public GameObject Fade;
    

    // Update is called once per frame
    void Update()
    {
        if(Fade.GetComponent<Fade>().Fading==true&&Fade.GetComponent<Image>().color == new Color(0,0,0,1))
        {
            GameManager.instance.SelectScene(9);
        }
    }
}
