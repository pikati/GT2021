using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonController : MonoBehaviour
{
    public void Resume()
    {
        Singleton<GameManager>.Instance.ChangeGameState(GameManager.GameState.Play);
    }

    public void Retry()
    {
        Singleton<SceneChanger>.Instance.ReloadScene();
    }

    public void Option()
    {

    }

    public void ToTitle()
    {
        Singleton<SceneChanger>.Instance.SceneChange("Title");
    }
}
