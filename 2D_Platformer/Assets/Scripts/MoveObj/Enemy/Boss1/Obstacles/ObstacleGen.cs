using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGen : MonoBehaviour
{
    public float genTime;
    [SerializeField]
    public GameObject[] Obstacles;
    
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > genTime)
        {
            GameObject.Instantiate(Obstacles[Random.Range(0,Obstacles.Length)],transform.position,Quaternion.identity);
            time = 0;
        }
    }
}
