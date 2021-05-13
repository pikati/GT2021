using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCount : Singleton<ClearCount>
{
    private List<GameObject> goalImages = new List<GameObject>(5);
    private int goalCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        goalImages.Add(transform.GetChild(1).gameObject);
        goalImages.Add(transform.GetChild(2).gameObject);
        goalImages.Add(transform.GetChild(3).gameObject);
        goalImages.Add(transform.GetChild(4).gameObject);
        goalImages.Add(transform.GetChild(5).gameObject);
        for(int i = Singleton<ClearChecker>.Instance.ClearNum; i < 5; i++)
        {
            goalImages[i].SetActive(false);
        }
    }

    public void UpdateClearNum()
    {
        //ゴールの画像オブジェの子供にあるMaskオブジェを取得しそれを非表示に
        goalImages[goalCount++].transform.GetChild(1).gameObject.SetActive(false);
    }
}
