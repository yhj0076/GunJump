using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloAnimeController : MonoBehaviour
{
    Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anime.SetBool("isApproach",true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anime.SetBool("isApproach",false);
    }
}
