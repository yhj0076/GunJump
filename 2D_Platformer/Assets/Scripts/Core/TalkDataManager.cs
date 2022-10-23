using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkDataManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "여긴 어디죠?", "내가 살던 곳은 이런 느낌이 아니었는데..." });
        talkData.Add(1001, new string[] { "고장난 기체인 듯 하다..." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
}
