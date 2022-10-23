using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnOffController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] Buttons;

    private void Start()
    {
        Cleared();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i<Buttons.Length;i++)
        {
            ButtonOnOff(i);
        }
    }

    void ButtonOnOff(int index)
    {
        if (Buttons[index].transform.GetChild(1).GetComponent<Image>().color.a < 1)
        {
            Buttons[index].GetComponent<Button>().enabled = false;
        }
        else
        {
            Buttons[index].GetComponent<Button>().enabled = true;
        }
    }

    void Cleared()
    {
        for(int i = 0;i< PlayerPrefs.GetInt("ClearedCount", -1); i++)
        {
            Buttons[i].transform.GetChild(1).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
}
