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
        Option,
        Clear
    }

    private GameObject clearText;
    private GameObject pauseUI;
    private GameObject optionUI;
    private PauseController pc;
    private InputController ic;
    public GameState gameState { get; private set; } = GameState.Play;
    private void Start()
    {
        Singleton<Fade>.Instance.FadeOut();
        clearText = GameObject.Find("ClearText");
        pauseUI = GameObject.Find("PauseUI");
        optionUI = GameObject.Find("OptionUI");
        ChangeGameState(GameState.Play);
        ic = Singleton<InputController>.Instance;
        pc = GameObject.Find("UI").GetComponent<PauseController>();
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
            if (gameState == GameState.Option)
            {
                ChangeGameState(GameState.Pause);
            }
        }
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            if (ic.A)
            {
                Singleton<SceneChanger>.Instance.SceneChange();
            }
        }
        if(ic.RT && ic.LT)
        {
            Quit();
        }
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;
        switch (state)
        {
            case GameState.Play:
                pauseUI.SetActive(false);
                optionUI.SetActive(false);
                clearText.SetActive(false);
                break;
            case GameState.Pause:
                pauseUI.SetActive(true);
                optionUI.SetActive(false);
                clearText.SetActive(false);
                Invoke("Pause", 0.1f);
                break;
            case GameState.Option:
                pauseUI.SetActive(false);
                optionUI.SetActive(true);
                clearText.SetActive(false);
                break;
            case GameState.Clear:
                pauseUI.SetActive(false);
                optionUI.SetActive(false);
                clearText.SetActive(true);
                break;
        }

    }

    private void Pause()
    {
        pc.BeginPause();
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
