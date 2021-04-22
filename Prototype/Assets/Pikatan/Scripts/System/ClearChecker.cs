using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChecker : Singleton<ClearChecker>
{
    [SerializeField]
    private int checkPointCount = 1;
    public bool IsClear { get; private set; } = false;
    private GameObject clearText;
    // Start is called before the first frame update
    void Start()
    {
        clearText = GameObject.Find("ClearText");
        clearText.SetActive(false);
    }

    public void ReachChechkPoint()
    {
        checkPointCount--;
        if(checkPointCount <= 0)
        {
            IsClear = true;
            clearText.SetActive(true);
        }
    }
}
