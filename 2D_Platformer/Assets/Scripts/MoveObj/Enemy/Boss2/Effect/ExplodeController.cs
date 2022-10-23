using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    public GameObject ArmRazor;

    bool[] effectOn = new bool[5];

    int positionFlip = 0;
    public float time = 0;
    private void OnEnable()
    {
        time = 0;
        effectOn[0] = false;
        effectOn[1] = false;
        effectOn[2] = false;
        effectOn[3] = false;
        effectOn[4] = false;
        if (transform.parent.GetComponent<SpriteRenderer>().flipX == true)
        {
            positionFlip = -1;
        }
        else
        {
            positionFlip = 1;
        }
        transform.GetChild(0).localPosition = new Vector2(positionFlip * 1.7f, -1.87f);
        transform.GetChild(1).localPosition = new Vector2(positionFlip * 3.7f, -1.87f);
        transform.GetChild(2).localPosition = new Vector2(positionFlip * 5.7f, -1.87f);
        transform.GetChild(3).localPosition = new Vector2(positionFlip * 7.7f, -1.87f);
        transform.GetChild(4).localPosition = new Vector2(positionFlip * 9.7f, -1.87f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        ActiveExplode();
        if (transform.GetChild(4).gameObject.activeSelf == true)
        {
            if (transform.GetChild(4).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        effectOn[0] = true;
        effectOn[1] = true;
        effectOn[2] = true;
        effectOn[3] = true;
        effectOn[4] = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
    }

    void ActiveExplode()
    {
        if (/*ArmRazor.GetComponent<ArmRazorEffect_Hydey>().*/time >= 0.2f && effectOn[0] == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            effectOn[0] = true;
        }
        if (/*ArmRazor.GetComponent<ArmRazorEffect_Hydey>().*/time >= 0.4f && effectOn[1] == false)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            effectOn[1] = true;
        }
        if (/*ArmRazor.GetComponent<ArmRazorEffect_Hydey>().*/time >= 0.6f && effectOn[2] == false)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            effectOn[2] = true;
        }
        if (/*ArmRazor.GetComponent<ArmRazorEffect_Hydey>().*/time >= 0.8f && effectOn[3] == false)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            effectOn[3] = true;
        }
        if (/*ArmRazor.GetComponent<ArmRazorEffect_Hydey>().*/time >= 1 && effectOn[4] == false)
        {
            transform.GetChild(4).gameObject.SetActive(true);
            effectOn[4] = true;
        }
    }
}
