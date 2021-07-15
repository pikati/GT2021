using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePointController : MonoBehaviour
{
    private int rotatePointNum = -1;
    private int updateRotatePointNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        var points = GameObject.FindGameObjectsWithTag("RotatePoint");
        rotatePointNum = points.Length;
    }

    public void Bake()
    {
        if(++updateRotatePointNum == rotatePointNum)
        {
            Singleton<NavMeshBaker>.Instance.Bake();
            updateRotatePointNum = 0;
        }
    }
}
