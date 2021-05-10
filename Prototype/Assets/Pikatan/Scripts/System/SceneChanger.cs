using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : Singleton<SceneChanger>
{
    [SerializeField]
    private string nextSceneName;
    public void SceneChange()
    {
        if (nextSceneName == "non") return;
        Singleton<Fade>.Instance.FadeIn(nextSceneName);
    }

    public void SceneChange(string nextSceneName)
    {
        Singleton<Fade>.Instance.FadeIn(nextSceneName);
    }

    public void ReloadScene()
    {
        Singleton<Fade>.Instance.FadeIn(SceneManager.GetActiveScene().name);
    }
}
