using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : Singleton<SceneChanger>
{
    [SerializeField]
    private string nextSceneName;
    private bool isChange = false;

    public void SceneChange()
    {
        if (nextSceneName == "non") return;
        if (isChange) return;
        isChange = true;
        Singleton<Fade>.Instance.FadeIn(nextSceneName);
    }

    public void SceneChange(string nextSceneName)
    {
        if (isChange) return;
        isChange = true;
        Singleton<Fade>.Instance.FadeIn(nextSceneName);
    }

    public void ReloadScene()
    {
        if (isChange) return;
        isChange = true;
        Singleton<Fade>.Instance.FadeIn(SceneManager.GetActiveScene().name);
    }
}
