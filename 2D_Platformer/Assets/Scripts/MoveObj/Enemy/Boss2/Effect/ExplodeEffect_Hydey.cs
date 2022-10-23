using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect_Hydey : MonoBehaviour
{
    Animator anime;

    private void OnEnable()
    {
        anime = GetComponent<Animator>();
        anime.SetBool("Boom", true);
    }

    private void Update()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        anime.SetBool("Boom", false);
    }
}
