using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisStateController : Singleton<AxisStateController>
{
    public enum AxisStateEnum
    {
        NoRotate,
        Rotating
    }

    public AxisStateEnum AxisState { get; set; } = AxisStateEnum.NoRotate;
}
