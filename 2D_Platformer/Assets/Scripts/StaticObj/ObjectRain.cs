using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 최초 작성자 : 이호준
 * 최종 작성일 : 2021.03.10
 * 최종 수정자 : 이호준
 * 파일 이름 : ObjectRain.cs
 * 클래스 내용 : 랜덤한 위치에서 오브젝트가 떨어지는 함정 클래스
 */

public class ObjectRain : MonoBehaviour
{
    public GameObject FallingObject1;
    public GameObject FallingObject2;
    public GameObject FallingObject3;

    private float width;
    public float TimeRepeat;
    public float TimeDestroy;
    private float nextTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        BoxCollider2D range = GetComponent<BoxCollider2D>();
        width = range.size.x; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>nextTime)
        {
            nextTime = Time.time + TimeRepeat;
            Fall();
        }
    }

    void Fall()
    {
        float chooseObj = Random.Range(0, 3);
        float randPosX = Random.Range(this.transform.position.x - width / 2,
                                      this.transform.position.x + width / 2);
        switch (chooseObj)
        {
            case 0:
                var FallObj1 = Instantiate(FallingObject1, new Vector2(randPosX, this.transform.position.y), Quaternion.identity);
                Destroy(FallObj1, TimeDestroy);
                break;
            case 1:
                var FallObj2 = Instantiate(FallingObject2, new Vector2(randPosX, this.transform.position.y), Quaternion.identity);
                Destroy(FallObj2, TimeDestroy);
                break;
            case 2:
                var FallObj3 = Instantiate(FallingObject3, new Vector2(randPosX, this.transform.position.y), Quaternion.identity);
                Destroy(FallObj3, TimeDestroy);
                break;
            default:
                break;
        }
    }    
}
