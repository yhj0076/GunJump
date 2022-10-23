using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBreakableWall : MonoBehaviour
{
    public GameObject Breaked1;
    public GameObject Breaked2;
    public GameObject Breaked3;
    public GameObject Breaked4;
    public GameObject Breaked5;
    public GameObject Breaked6;
    public void BreakEffect(Vector3 cellPosition)
    {
        var BreakedWall1 = Instantiate(Breaked1, new Vector3 (cellPosition.x + 0.5f, cellPosition.y + 0.5f), Quaternion.identity);
        var BreakedWall2 = Instantiate(Breaked2, new Vector3 (cellPosition.x + 0.5f, cellPosition.y + 0.5f), Quaternion.identity);
        var BreakedWall3 = Instantiate(Breaked3, new Vector3 (cellPosition.x + 0.5f, cellPosition.y + 0.5f), Quaternion.identity);
        var BreakedWall4 = Instantiate(Breaked4, new Vector3 (cellPosition.x + 0.5f, cellPosition.y + 0.5f), Quaternion.identity);
        var BreakedWall5 = Instantiate(Breaked5, new Vector3 (cellPosition.x + 0.5f, cellPosition.y + 0.5f), Quaternion.identity);
        var BreakedWall6 = Instantiate(Breaked6, new Vector3 (cellPosition.x + 0.5f, cellPosition.y + 0.5f), Quaternion.identity);
        Destroy(BreakedWall1, 0.5f);
        Destroy(BreakedWall2, 0.5f);
        Destroy(BreakedWall3, 0.5f);
        Destroy(BreakedWall4, 0.5f);
        Destroy(BreakedWall5, 0.5f);
        Destroy(BreakedWall6, 0.5f);
    }
}
