using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisState : Singleton<AxisState>
{
    public enum AxisStateEnum
    {
        NoRotate,
        Rotating
    }

    public AxisStateEnum NowAxisState { get; set; } = AxisStateEnum.NoRotate;
}
