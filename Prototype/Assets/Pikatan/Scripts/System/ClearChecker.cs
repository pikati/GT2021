using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChecker : Singleton<ClearChecker>
{
    public int checkPointCount = 1;
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
            Singleton<SoundManager>.Instance.StopBgm();
            Singleton<SoundManager>.Instance.PlayBgmByName("result");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().ClearRotation();
            IsClear = true;
            Singleton<GameManager>.Instance.ChangeGameState(GameManager.GameState.Clear);

        }
    }
}
