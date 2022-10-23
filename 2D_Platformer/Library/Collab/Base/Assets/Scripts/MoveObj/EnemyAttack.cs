using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject Range;
    private Vector2 RangePoint; 
    private Vector2 fireTransform;
    private LineRenderer bulletLine;
    PlayerController playerController;
    

    private AudioSource gunAudioPlayer;
    public AudioClip shotClip;

    public bool TurretMode;
    public int damage;
    public float MaxDistance;
    public float BackOff;

    public float FireRate = 0.5f;
    public float lastFireTime = 0;



    void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLine = GetComponent<LineRenderer>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        bulletLine.positionCount = 2;
        bulletLine.enabled = false;
    }

    void Update()
    {
        fireTransform = firePoint.transform.position;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Fire();
        }
    }
    
    public void Fire()
    {
        if(Time.time>= lastFireTime + FireRate)
        {
            Shot();
            lastFireTime = Time.time;
        }
    }
    
    private void Shot()
    {
        
        RaycastHit2D hit;
        Vector2 hitPosition = Vector2.zero;
        Vector2 ShotStart = new Vector2(firePoint.transform.position.x,
                                        firePoint.transform.position.y);
        RangePoint = Range.transform.position;
        Vector2 offset = RangePoint - ShotStart;
        Vector2 ShotEnd = offset.normalized;

        Debug.Log("s1:"+ShotStart);
        Debug.Log("e1:"+ShotEnd);

        if (hit = Physics2D.Raycast(ShotStart,ShotEnd,MaxDistance))
        {
            Debug.Log(ShotStart);
            Debug.Log(ShotEnd);
           
            if (hit.collider.tag == "Player")
            {
                Debug.Log("잡음");
                hit.collider.gameObject.GetComponent<PlayerController>().Damaged(damage);
                
            }
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = RangePoint;
        }
        Debug.DrawRay(ShotStart, ShotEnd, Color.red);

        StartCoroutine(ShotEffect(hitPosition));
    }

    private IEnumerator ShotEffect(Vector2 hitPosition)
    {
        gunAudioPlayer.PlayOneShot(shotClip);

        bulletLine.SetPosition(0, fireTransform);
        bulletLine.SetPosition(1, hitPosition);
        bulletLine.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLine.enabled = false;
    }
}

