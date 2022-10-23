using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRazorEffect_Hydey : MonoBehaviour
{
    public float time = 0;
    public float Speed;     // 높을수록 느리다.
    private void OnEnable()
    {
        if(transform.parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.localPosition = new Vector2(1.05f, 1.17f);
        }
        else
        {
            transform.localPosition = new Vector2(-1.05f,1.17f);
        }
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (transform.parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.rotation = Quaternion.Euler(0,0,-130-(35*(time/Speed)));         // -130 ~ -165

        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,-55+(42*(time/Speed)));         // -55 ~ -13
        }

        if((time/Speed)>=1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        time = 0;
        if (transform.parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.rotation = Quaternion.Euler(0,0,-130);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -55);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BoomEffect")
        {
            collision.GetComponent<Animator>().SetBool("Boom",true);
        }
    }
}
