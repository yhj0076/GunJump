using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCbuttons : MonoBehaviour
{
    public void _esc()
    {
        UIManager.instance.pause = 1;
    }

    public void Resume()
    {
        UIManager.instance.pause = -1;
    }

    public void Restart()
    {
        GameManager.instance.GameRestart();
    }

    public void Quit()
    {
        GameManager.instance._ToMainMenu();
    }
}
