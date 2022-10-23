using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextStage : MonoBehaviour
{
    public GameObject cam;
    public GameObject fade;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().StageClear = true;
            cam.GetComponent<MainCamFollow>().StopCam = true;
            fade.GetComponent<Fade>().Fading = true;
            switch(SceneManager.GetActiveScene().buildIndex)
            {
                case 2: //Stage1
                    PlayerPrefs.SetInt("ClearedCount", 0);
                    break;
                case 3: //Boss1
                    PlayerPrefs.SetInt("ClearedCount", 1);
                    break;
                case 4: //Stage2
                    PlayerPrefs.SetInt("ClearedCount", 2);
                    break;
                case 5: //Boss2
                    PlayerPrefs.SetInt("ClearedCount", 3);
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.NextGame();
    }
}
