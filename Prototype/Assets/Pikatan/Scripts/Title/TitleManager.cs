using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public enum DispState
    {
        Title,
        Select,
        Option
    }

    private Fade fade;
    private GameObject titleObj;
    private GameObject selectObj;
    private GameObject optionObj;
    private DispState dispState;
    // Start is called before the first frame update
    void Start()
    {
        fade = Singleton<Fade>.Instance;
        titleObj = GameObject.Find("Title");
        selectObj = GameObject.Find("StageSelect");
        optionObj = GameObject.Find("Option");
        ChangeDisp(DispState.Title);
    }

    // Update is called once per frame
    void Update()
    {
        if(DispState.Title != dispState)
        {
            if(Singleton<InputController>.Instance.B)
            {
                ChangeDisp(DispState.Title);
            }
        }
    }

    void ChangeDisp(DispState state)
    {
        dispState = state;
        switch (dispState)
        {
            case DispState.Title:
                titleObj.SetActive(true);
                selectObj.SetActive(false);
                optionObj.SetActive(false);
                Singleton<DefaultButtonSelector>.Instance.SelectButton(DispState.Title);
                break;
            case DispState.Select:
                titleObj.SetActive(false);
                selectObj.SetActive(true);
                optionObj.SetActive(false);
                Singleton<DefaultButtonSelector>.Instance.SelectButton(DispState.Select);
                break;
            case DispState.Option:
                titleObj.SetActive(false);
                selectObj.SetActive(false);
                optionObj.SetActive(true);
                Singleton<DefaultButtonSelector>.Instance.SelectButton(DispState.Option);
                break;
        }

    }


    public void StartGame()
    {
        ChangeDisp(DispState.Select);
    }

    public void Option()
    {
        ChangeDisp(DispState.Option);
    }

    public void ExitGame()
    {
        if (fade.isFading) return;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
