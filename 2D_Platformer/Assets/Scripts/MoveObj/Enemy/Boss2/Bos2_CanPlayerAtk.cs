using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bos2_CanPlayerAtk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            transform.parent.GetChild(0).GetComponent<ArmAnimation_Hydey>().startGame = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            transform.parent.GetComponent<Boss2_Health>().canHit = true;
            transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hydey_Die")==false)
        {
            transform.parent.GetComponent<Boss2_Health>().canHit = false;
            Color colorCantAtk;
            ColorUtility.TryParseHtmlString("#63FBFF", out colorCantAtk);
            transform.parent.GetComponent<SpriteRenderer>().color = colorCantAtk;
        }
    }
}
