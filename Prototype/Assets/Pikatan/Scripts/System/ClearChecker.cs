using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChecker : Singleton<ClearChecker>
{
    private int checkPointCount = 1;
    public int ClearNum => checkPointCount;
    public bool IsClear { get; private set; } = false;

    new private void Awake()
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
            Singleton<SoundManager>.Instance.StopBgm();
            Singleton<SoundManager>.Instance.PlayBgmByName("result");
            IsClear = true;
        }
    }
}
