using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChecker : Singleton<ClearChecker>
{
    private GameObject obj;
    private GameObject effect;
    public int checkPointCount = 1;
    public int ClearNum => checkPointCount;
    public bool IsClear { get; private set; } = false;

    new private void Awake()
    {
        GameObject[] goalObjs = GameObject.FindGameObjectsWithTag("Goal");
        checkPointCount = goalObjs.Length;
        obj = Resources.Load("Hanabi_Effect") as GameObject;
    }

    public void ReachChechkPoint()
    {
        checkPointCount--;
        Singleton<ClearCount>.Instance.UpdateClearNum();
        if(checkPointCount <= 0 && !IsClear)
        {
            Singleton<SoundManager>.Instance.StopBgm();
            Singleton<SoundManager>.Instance.PlayBgmByName("result");
            var p = GameObject.FindGameObjectWithTag("Player");
            p.GetComponent<PlayerMove>().ClearRotation();
            IsClear = true;
            Singleton<GameManager>.Instance.ChangeGameState(GameManager.GameState.Clear);
            effect = Instantiate(obj, p.transform.position + new Vector3(0, 5, 5), Quaternion.identity);
        }
    }
}
