using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    
    public void CnangeScene(string nextSceneName)
    {
        Singleton<Fade>.Instance.FadeIn(nextSceneName);
    }
}
