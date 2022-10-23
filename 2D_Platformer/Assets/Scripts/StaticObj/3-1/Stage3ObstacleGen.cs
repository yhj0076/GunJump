using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3ObstacleGen : MonoBehaviour
{
    [SerializeField]
    public GameObject[] Obs;
    public GameObject BackGround;
    float time = 0;
    public float GenTime;

    public GameObject timer;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > GenTime&&((timer.GetComponent<Timer3_1>().Timer_Minute<1 && timer.GetComponent<Timer3_1>().Timer_Second<5)==false))
        {
            int choice = Random.Range(0,Obs.Length);
            GameObject.Instantiate(Obs[choice], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            time = 0;
        }
    }
}
