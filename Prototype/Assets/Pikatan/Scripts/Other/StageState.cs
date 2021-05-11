using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageState : Singleton<StageState>
{
    public enum StageStateEnum
    { 
        Player,
        Rotate
    }

    [SerializeField]
    private StageStateEnum state = StageStateEnum.Rotate;
    public StageStateEnum NowStageState => state;

    private void Update()
    {
        if (Singleton<InputController>.Instance.X)
        {
            ChangeStageState();
        }
    }

    public void ChangeStageState()
    {
        //if(state == StageStateEnum.Player)
        //{
        //    state = StageStateEnum.Rotate;
        //    Singleton<RotatePointSelector>.Instance.SetSelectableObjects();
        //}
        //else
        //{
        //    state = StageStateEnum.Player;
        //}
    }
}
