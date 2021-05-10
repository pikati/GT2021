using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//クリア時のテキスト表示やエフェクト発生などの管理、シーンの遷移の実行などする
public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Play,
        Pause,
        Clear
    }

    private GameObject clearText;
    private GameObject pauseUI;
    private InputController ic;
    public GameState gameState { get; private set; } = GameState.Play;
    private void Start()
    {
        Singleton<Fade>.Instance.FadeOut();
        clearText = GameObject.Find("ClearText");
        clearText.SetActive(false);
        pauseUI = GameObject.Find("PauseUI");
        pauseUI.SetActive(false);
        ic = Singleton<InputController>.Instance;
    }

    private void Update()
    {
        if(ic.StartPress)
        {
            if(gameState == GameState.Play)
            {
                ChangeGameState(GameState.Pause);
            }
            else if(gameState == GameState.Pause)
            {
                ChangeGameState(GameState.Play);
            }
        }
        if(ic.B)
        {
            if(gameState == GameState.Pause)
            {
                ChangeGameState(GameState.Play);
            }
        }
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            if (ic.A)
            {
                Singleton<SceneChanger>.Instance.SceneChange();
            }
        }
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;
        switch (state)
        {
            case GameState.Play:
                pauseUI.SetActive(false);
                break;
            case GameState.Pause:
                pauseUI.SetActive(true);
                break;
            case GameState.Clear:
                pauseUI.SetActive(false);
                clearText.SetActive(true);
                break;
        }

    }

    public void StageClear()
    {
        
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
