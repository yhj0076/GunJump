using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public TalkDataManager TalkData;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction=false;
    public int talkIndex;
    public void Action(GameObject scanObj)
    {
        
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.IsNpc);
        
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkList = TalkData.GetTalk(id, talkIndex);

        if(talkList == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if(isNpc)
        {
            talkText.text = talkList;
        }
        else
        {
            talkText.text = talkList;
        }
        isAction = true;
        talkIndex++;
    }
}
