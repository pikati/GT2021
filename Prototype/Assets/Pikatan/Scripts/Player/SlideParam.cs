using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideParam
{
    private Vector3 direction;
    public Vector3 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
            if(direction.x > 1.0f)
            {
                direction.x = 1.0f;
            }
            if(direction.x < -1.0f)
            {
                direction.x = -1.0f;
            }
            if(direction.z > 1.0f)
            {
                direction.z = 1.0f;
            }
            if(direction.z < -1.0f)
            {
                direction.z = -1.0f;
            }
        }
    }
}
