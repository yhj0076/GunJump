using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRailAnime : MonoBehaviour
{
    Animator anime;
    Boss1Controller bossCon;
    CapsuleCollider2D col;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        bossCon = GetComponentInParent<Boss1Controller>();
        col = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        starting();
        stopping();
    }
    

    void stopping()
    {
        if (col.name == "stopPos")
        {
            anime.SetBool("IsWalking", false);
        }
    }

    void starting()
    {
        anime.SetBool("IsWalking", true);
    }
}
