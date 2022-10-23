using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGenerator : MonoBehaviour
{
    public GameObject Missile;
    // Start is called before the first frame update
    void OnEnable()
    {
        GenMissile();
        Invoke("GenMissile",0.5f);
        Invoke("GenMissile",1);
        Invoke("stateChange",1);
    }

    void GenMissile()
    {
        GameObject.Instantiate(Missile, new Vector2(Random.Range(-5, 5)+transform.position.x, transform.position.y), Quaternion.identity);
    }
    void stateChange()
    {
        Debug.Log("대기모드");
        transform.parent.GetComponent<Boss1_Controller>().state = Boss1_Controller.B1_STATE.B1_IDLE;
        transform.parent.GetComponent<Animator>().SetBool("isMissile", false);
        gameObject.SetActive(false);
    }
}
