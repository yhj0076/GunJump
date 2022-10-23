using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHand : MonoBehaviour
{
    public float GrabSpeed;
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right*GrabSpeed;
    }

    private void Update()
    {
        if(transform.localPosition.x>6)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * GrabSpeed;
        }

        if(transform.localPosition.x<2.5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.localPosition = new Vector3(2.5f,-0.17f,0);
        transform.parent.GetComponent<Boss1_Controller>().state = Boss1_Controller.B1_STATE.B1_IDLE;
        transform.parent.GetComponent<Animator>().SetBool("isGrab", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = transform.position+new Vector3(0.5f,0,0);
        }
    }
}
