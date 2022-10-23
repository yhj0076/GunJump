using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectMaker : MonoBehaviour
{
    public GameObject Effect;
    float time = 0;
    float width;
    private void Awake()
    {
        BoxCollider2D sizeCollider = GetComponent<BoxCollider2D>();
        width = sizeCollider.size.x;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time > 0.5f)
        {
            GameObject.Instantiate(Effect, new Vector2(Random.Range(transform.position.x - width / 2, transform.position.x + width / 2),transform.position.y),Quaternion.identity).transform.parent = transform.parent;
            time = 0;
        }
    }
}
