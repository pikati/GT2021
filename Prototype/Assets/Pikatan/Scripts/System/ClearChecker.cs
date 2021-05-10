using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChecker : Singleton<ClearChecker>
{
    private int checkPointCount = 1;
    public int ClearNum => checkPointCount;
    public bool IsClear { get; private set; } = false;

    private void Start()
    {
        GameObject[] goalObjs = GameObject.FindGameObjectsWithTag("Goal");
        checkPointCount = goalObjs.Length;
    }

    public void ReachChechkPoint()
    {
        checkPointCount--;
        Singleton<ClearCount>.Instance.UpdateClearNum();
        if(checkPointCount <= 0)
        {
            Singleton<GameManager>.Instance.ChangeGameState(GameManager.GameState.Clear);
            IsClear = true;
        }
    }
}
