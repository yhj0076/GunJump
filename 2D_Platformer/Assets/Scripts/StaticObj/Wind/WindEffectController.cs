using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectController : MonoBehaviour
{
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 power= transform.parent.GetComponent<WindZone>().WindPower;
        rigid.velocity = new Vector2(power.x/5,power.y/5); 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
