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
    private InputController ic;
    private ClearCount clearCount;
    private SoundManager sm;
    public GameState gameState { get; private set; } = GameState.Play;
    private void Start()
    {
        Singleton<Fade>.Instance.FadeOut();
        sm = Singleton<SoundManager>.Instance;
        sm.PlayBgmByName("game");
        clearText = GameObject.Find("ClearUI");
        pauseUI = GameObject.Find("PauseUI");
        optionUI = GameObject.Find("OptionUI");
        clearCount = GameObject.Find("GoalUI").GetComponent<ClearCount>();
        ChangeGameState(GameState.Play);
        ic = Singleton<InputController>.Instance;
    }

    private void Update()
    {
        if(ic.StartPress)
        {
            if(gameState == GameState.Play)
            {
                ChangeGameState(GameState.Pause);
                sm.PlaySeByName("decide");
            }
            else if(gameState == GameState.Pause)
            {
                ChangeGameState(GameState.Play);
                sm.PlaySeByName("cancel");
            }
        }
        if(ic.B)
        {
            if(gameState == GameState.Pause)
            {
                ChangeGameState(GameState.Play);
                sm.PlaySeByName("cancel");
            }
            if (gameState == GameState.Option)
            {
                ChangeGameState(GameState.Pause);
                sm.PlaySeByName("cancel");
            }
        }
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            if (ic.A)
            {
                Singleton<SceneChanger>.Instance.SceneChange();
                sm.PlaySeByName("decide");
            }
        }
        if(ic.RT && ic.LT)
        {
            Quit();
        }

        if(gameState == GameState.Pause || gameState == GameState.Option)
        {
            clearCount.ChangeState(ClearCount.ImageState.Visible);
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

    public void Pause()
    {
        ChangeGameState(GameState.Pause);
        sm.PlaySeByName("decide");
    }

    private void Play()
    {
        ChangeGameState(GameState.Play);
        sm.PlaySeByName("decide");
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
