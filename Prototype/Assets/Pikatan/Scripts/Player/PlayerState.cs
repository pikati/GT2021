using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum PlayerStateEnum
    { 
        Move,
        Slide,
        Max
    }

    public PlayerStateEnum state { get; set; } = PlayerStateEnum.Move;
}
