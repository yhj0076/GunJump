using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    Animator anime;
    Rigidbody2D rigid;
    PlayerController playerController;
    WindZone windZone;

    void Awake()
    {
        anime = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        windZone = GetComponent<WindZone>();
    }

    private void Update()
    {
        CheckFloor();
    }

    void CheckFloor()
    {
        Vector3 frontPos = new Vector3(0.3f, 0, 0);
        Debug.DrawRay(transform.position + frontPos, Vector2.down * 0.8f);
        Debug.DrawRay(transform.position - frontPos, Vector2.down * 0.8f);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + frontPos, Vector2.down, 0.8f, LayerMask.GetMask("Platform", "Obstacles In Boss"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position - frontPos, Vector2.down, 0.8f, LayerMask.GetMask("Platform", "Obstacles In Boss"));
        if (hit1.collider == null&&hit2.collider == null)    
        {
            anime.SetBool("isJump", true);
            playerController.isGrounded = false;
        }
        else
        {
            anime.SetBool("isJump", false);
            playerController.isGrounded = true;
        }
    }
}
