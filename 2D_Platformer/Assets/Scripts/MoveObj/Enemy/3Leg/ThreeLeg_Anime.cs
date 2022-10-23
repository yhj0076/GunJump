using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeLeg_Anime : MonoBehaviour
{
    ThreeLegController TL;
    Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        TL = GetComponent<ThreeLegController>();
        anime = GetComponent<Animator>();
        anime.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (TL.state)
        {
            case ThreeLegController.THREELEG_STATE.TL_WALK:
                anime.SetBool("isJump",false);
                anime.SetBool("isBlast",false);
                break;
            case ThreeLegController.THREELEG_STATE.TL_JUMP:
            case ThreeLegController.THREELEG_STATE.TL_JUMP_AFTER:
                anime.SetBool("isJump", true);
                break;
            case ThreeLegController.THREELEG_STATE.TL_ATTACK:
            case ThreeLegController.THREELEG_STATE.TL_ATK_AFTER:
                anime.SetBool("isBlast",true);
                break;
            case ThreeLegController.THREELEG_STATE.TL_DEAD:
                anime.SetBool("isDead", true);
                break;
            default:
                Debug.LogError("예외 발생!!");
                break;
        }

        if (anime.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {            Destroy(gameObject);
        }

        if (anime.GetCurrentAnimatorStateInfo(0).IsName("endBlast"))
        {
            anime.SetBool("isBlast",false);
            TL.state = ThreeLegController.THREELEG_STATE.TL_WALK;
        }
    }
}
