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
        SceneManager.LoadScene(nextSceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
