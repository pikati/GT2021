using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : Singleton<StageClearManager>
{
    public enum ClearState
    { 
        Non,
        Play,
        Clear
    }

    private ClearState[] clearStates;

    new private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        clearStates = new ClearState[GameObject.Find("StageButtons").GetComponent<StageSelectUIController>().StageNum];
        for(int i = 0; i < clearStates.Length; i++)
        {
            clearStates[i] = ClearState.Non;
        }
    }

    public void PlayStage(int n)
    {
        clearStates[n] = ClearState.Play;
    }

    public void ClearStage(int n)
    {
        clearStates[n] = ClearState.Clear;
    }

    public ClearState GetClearState(int n)
    {
        return clearStates[n];
    }
}
