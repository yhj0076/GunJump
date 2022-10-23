using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeLeg_Maker : MonoBehaviour
{
    float time = 0;

    public GameObject ThreeLeg;
    public float spawnRate;
    
    // Start is called before the first frame update
    void Start()
    {
        time = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        spawn();
    }

    void spawn()
    {
        time += Time.deltaTime;
        if (time >= spawnRate)
        {
            time = 0;
            var ThreeLegSpawn = Instantiate(ThreeLeg,transform.position,Quaternion.identity);
        }
    }
}
