using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChecker : Singleton<ClearChecker>
{
    [SerializeField]
    private int checkPointCount = 1;
    public int ClearNum => checkPointCount;
    public bool IsClear { get; private set; } = false; 
    
    public void ReachChechkPoint()
    {
        checkPointCount--;
        Singleton<ClearCount>.Instance.UpdateClearNum();
        if(checkPointCount <= 0)
        {
            Singleton<GameManager>.Instance.StageClear();
            IsClear = true;
        }
    }
}
