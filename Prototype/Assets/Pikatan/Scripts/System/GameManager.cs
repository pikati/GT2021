using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//クリア時のテキスト表示やエフェクト発生などの管理、シーンの遷移の実行などする
public class GameManager : Singleton<GameManager>
{
    private GameObject clearText;
    private InputController ic;
    private void Start()
    {
        clearText = GameObject.Find("ClearText");
        clearText.SetActive(false);
        ic = Singleton<InputController>.Instance;
    }

    private void Update()
    {
        if(ic.SelectPress)
        {
            Singleton<SceneChanger>.Instance.ReloadScene();
        }
        if(ic.StartPress)
        {
            Quit();
        }
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            if (ic.A)
            {
                Singleton<SceneChanger>.Instance.SceneChange();
            }
        }
    }

    public void StageClear()
    {
        clearText.SetActive(true);
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
