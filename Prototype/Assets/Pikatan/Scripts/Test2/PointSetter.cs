using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSetter : MonoBehaviour
{
    public MovePoint MovePoint { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        MovePoint = new MovePoint();
        MovePoint.SetPoint(gameObject.transform.position);
    }
}
