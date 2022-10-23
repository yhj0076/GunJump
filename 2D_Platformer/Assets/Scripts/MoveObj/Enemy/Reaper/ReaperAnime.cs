using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAnime : MonoBehaviour
{
    Reaper ine;
    Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        ine = GetComponent<Reaper>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (ine.state)
        {
            case Reaper.REAPER_STATE.RS_IDLE:
                anime.SetBool("Teleport", false);
                anime.SetBool("Attack", false);
                //anime.SetBool("TripleAttack", false);
                break;
            case Reaper.REAPER_STATE.RS_TELEPORT:
                anime.SetBool("Teleport", true);
                if(anime.GetCurrentAnimatorStateInfo(0).IsName("Ripper_Teleport2"))
                {
                    anime.SetBool("Teleport", false);
                    ine.state = Reaper.REAPER_STATE.RS_BEFORETHINKFORNT;
                }
                break;
            case Reaper.REAPER_STATE.RS_ATTACK:
                anime.SetBool("Attack", true);
                if(anime.GetCurrentAnimatorStateInfo(0).IsName("Ripper_Slash"))
                {
                    anime.SetBool("Attack", false);
                    ine.state = Reaper.REAPER_STATE.RS_BEFORETHINKFORNT;
                }
                break;
            case Reaper.REAPER_STATE.RS_DEAD:
                anime.SetBool("isDead", true);
                Destroy(ine.SlashP);
                if (anime.GetCurrentAnimatorStateInfo(0).IsName("End"))
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
