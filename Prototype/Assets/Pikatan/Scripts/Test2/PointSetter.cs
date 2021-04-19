using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSetter : MonoBehaviour
{
    public MovePoint MovePoint { get; private set; }
    void Awake()
    {
        MovePoint = new MovePoint();
        MovePoint.SetPoint(gameObject.transform.position);
    }
}
