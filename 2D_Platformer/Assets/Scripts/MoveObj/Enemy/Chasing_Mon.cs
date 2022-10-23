using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chasing_Mon : MonoBehaviour
{

    GameObject player;
    Vector3 pL;
    PlayerController playerController;
    Rigidbody2D rb2d;
    Vector3 monPosition;
    float dir;
    public float maxSpeed;
    public int Damage;
    // Start is called before the first frame update


    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        chase();
    }


    void chase()
    {
        monPosition = this.transform.position;
        pL =player.transform.position;
        dir = pL.x-monPosition.x;

        if(dir > 0)
        {
            rb2d.AddForce(Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            rb2d.AddForce(Vector2.left, ForceMode2D.Impulse);
        }

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        else if (rb2d.velocity.x < maxSpeed * (-1))
        {
            rb2d.velocity = new Vector2(maxSpeed * (-1), rb2d.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           playerController.Damaged(Damage);
        }
        else if(collision.gameObject.tag=="Trap")
        {
            Tilemap tileMap = collision.gameObject.GetComponentInChildren<Tilemap>();

            Vector3Int cellPosition = tileMap.WorldToCell(collision.contacts[0].point);
            
            tileMap.SetTile(cellPosition, null);
        }
        else if (collision.gameObject.tag == "Breakable Walls")
        {
            Tilemap tileMap = collision.gameObject.GetComponentInChildren<Tilemap>();

            Vector3Int cellPosition = tileMap.WorldToCell(collision.contacts[0].point);

            tileMap.SetTile(cellPosition, null);
            collision.gameObject.GetComponent<AfterBreakableWall>().BreakEffect(cellPosition);
        }
    }
}
