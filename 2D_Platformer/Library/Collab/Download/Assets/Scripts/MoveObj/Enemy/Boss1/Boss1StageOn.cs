using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1StageOn : MonoBehaviour
{
    public GameObject Stage;
    public GameObject Boss1;
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stage.SetActive(true);
    }
}
