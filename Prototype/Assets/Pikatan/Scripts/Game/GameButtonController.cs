using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameButtonController : MonoBehaviour
{
    
    public void Resume()
    {
        Singleton<GameManager>.Instance.ChangeGameState(GameManager.GameState.Play);
        Singleton<SoundManager>.Instance.PlaySeByName("decide");
    }

    public void Retry()
    {
        Singleton<SceneChanger>.Instance.ReloadScene();
        Singleton<SoundManager>.Instance.PlaySeByName("decide");
    }

    public void Option()
    {
        Singleton<GameManager>.Instance.ChangeGameState(GameManager.GameState.Option);
        Singleton<SoundManager>.Instance.PlaySeByName("decide");
    }

    public void ToTitle()
    {
        Singleton<SceneChanger>.Instance.SceneChange("Title");
        Singleton<SoundManager>.Instance.PlaySeByName("decide");
    }
}
