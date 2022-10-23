using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorEffect_Hydey : MonoBehaviour
{
    Animator anime;
    float time = 0;

    public int damage;

    private void OnEnable()
    {
        anime = GetComponent<Animator>();
        time = 0;
        anime.SetFloat("timer", 0);
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        anime.SetFloat("timer", time);
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
        }
    }
}
