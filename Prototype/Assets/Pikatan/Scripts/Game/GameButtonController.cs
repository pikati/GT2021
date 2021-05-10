using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameButtonController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseSelectObject;
    [SerializeField]
    private GameObject optionSelectObject;
    
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
        Singleton<GameManager>.Instance.ChangeGameState(GameManager.GameState.Option);
    }

    public void ToTitle()
    {
        Singleton<SceneChanger>.Instance.SceneChange("Title");
    }

    public void SetActiveButton(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Pause:
                EventSystem.current.SetSelectedGameObject(pauseSelectObject);
                break;
            case GameManager.GameState.Option:
                EventSystem.current.SetSelectedGameObject(optionSelectObject);
                break;
        }

    }
}
