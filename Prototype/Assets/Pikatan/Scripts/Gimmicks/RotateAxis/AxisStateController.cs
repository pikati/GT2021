using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//電柱管理のために使ってる
public class AxisStateController : MonoBehaviour
{
    private RotatePoint[] rps;
    public bool IsRotate { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        var objs = GameObject.FindGameObjectsWithTag("RotatePoint");
        int n = objs.Length;
        rps = new RotatePoint[n];
        for(int i = 0; i < n; i++)
        {
            rps[i] = objs[i].GetComponent<RotatePoint>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int n = rps.Length;
        IsRotate = false;
        for(int i = 0; i < n; i++)
        {
            if(rps[i].IsRotate)
            {
                IsRotate = true;
                return;
            }
        }
    }
}
