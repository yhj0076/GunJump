﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageFadeOut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<Fade>().Fading = false;
        }
    }
}
