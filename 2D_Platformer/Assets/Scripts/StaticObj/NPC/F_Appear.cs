using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Appear : MonoBehaviour
{
    Search search;
    TalkManager TM;
    public GameObject key_F;
    private void Update()
    {
        if(GameObject.Find("Search").GetComponent<Search>().scanObject == this.gameObject)
        {
            key_F.SetActive(true);
        }
        else
        {
            key_F.SetActive(false);
        }
    }
}
