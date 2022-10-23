using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGrabHand : MonoBehaviour
{
    public float grabSpeed;
    public bool Grabbed;

    public bool Shrink = true;

    CircleCollider2D col;

    [SerializeField]
    public Transform[] Waypoint;

    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Grab();
        if(transform.position == Waypoint[1].position)
        {
            col.enabled = false;
            getFree();
        }
        else
        {
            col.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Grabbed = true;
        }
    }

    void Grab()
    {
        if (Shrink == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Waypoint[0].position, grabSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Waypoint[1].position, grabSpeed * Time.deltaTime);
        }

        if (transform.position == Waypoint[0].position)
        {
            Shrink = true;
        }

        if (Grabbed)
        {
            GameObject.Find("Player").transform.position = transform.position;
        }
    }

    void getFree()
    {
        Grabbed = false;
    }
}
