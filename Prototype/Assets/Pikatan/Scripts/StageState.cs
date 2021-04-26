using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageState : Singleton<StageState>
{
    public enum StageStateEnum
    { 
        Player,
        Rotate
    }

    [SerializeField]
    private StageStateEnum state = StageStateEnum.Player;
    public StageStateEnum NowStageState => state;

    private void Update()
    {
        if (Keyboard.current.rKey.isPressed)
        {
            ChangeStageState();
        }
    }

    public void ChangeStageState()
    {
        if(state == StageStateEnum.Player)
        {
            state = StageStateEnum.Rotate;
            Singleton<RotatePointSelector>.Instance.SetSelectableObjects();
        }
        else
        {
            state = StageStateEnum.Player;
        }
    }
}
