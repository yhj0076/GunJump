using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour   // 길 따라 움직이는 발판(02.05)
{
    [SerializeField]
    public Transform[] Waypoint;
    [SerializeField]
    public float speed;
    int WaypointIndex = 0;

    void Start()
    {
        transform.position = Waypoint[WaypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    public void MovePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, Waypoint[WaypointIndex].transform.position, speed * Time.deltaTime);

        if(transform.position == Waypoint[WaypointIndex].transform.position)
        {
            WaypointIndex++;
        }
        if(WaypointIndex == Waypoint.Length)
        {
            WaypointIndex = 0;
        }
    }

}
