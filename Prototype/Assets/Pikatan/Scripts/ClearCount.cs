using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCount : Singleton<ClearCount>
{
    private Text num;
    // Start is called before the first frame update
    void Start()
    {
        num = GetComponent<Text>();
        num.text = "あと" + Singleton<ClearChecker>.Instance.ClearNum + "チェックポイント";
    }

    public void UpdateClearNum()
    {
        num.text = "あと" + Singleton<ClearChecker>.Instance.ClearNum + "チェックポイント";
    }
}
