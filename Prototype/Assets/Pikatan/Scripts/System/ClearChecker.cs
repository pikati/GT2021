using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChecker : Singleton<ClearChecker>
{
    [SerializeField]
    private int checkPointCount = 1;
    public bool IsClear { get; private set; } = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReachChechkPoint()
    {
        checkPointCount--;
        if(checkPointCount <= 0)
        {
            Singleton<GameManager>.Instance.StageClear();
            IsClear = true;
        }
    }
}
