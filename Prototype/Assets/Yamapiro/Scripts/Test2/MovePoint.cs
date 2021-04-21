using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint
{
    private Vector3 point = Vector3.zero;
    public Vector3 Point => point;

    public void SetPoint(Vector3 point)
    {
        this.point = point;
    }
}
