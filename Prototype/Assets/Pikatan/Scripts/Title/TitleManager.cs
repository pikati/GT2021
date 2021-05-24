﻿using System.Collections;
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
    private GameObject cursorObj;
    private DispState dispState;
    private SoundManager sm;

    void Start()
    {
        fade = Singleton<Fade>.Instance;
        sm = Singleton<SoundManager>.Instance;
        titleObj = GameObject.Find("TitleUI");
        selectObj = GameObject.Find("StageSelectUI");
        optionObj = GameObject.Find("OptionUI");
        cursorObj = GameObject.Find("SelectCursorUI");
        ChangeDisp(DispState.Title);
        sm.StopBgm();
        sm.PlayBgmByName("title");
    }

    // Update is called once per frame
    void Update()
    {
        if(DispState.Title != dispState)
        {
            if(Singleton<InputController>.Instance.B)
            {
                ChangeDisp(DispState.Title);
                sm.PlaySeByName("cancel");
            }
        }
    }

    private void ChangeDisp(DispState state)
    {
        dispState = state;
        switch (dispState)
        {
            case DispState.Title:
                titleObj.SetActive(true);
                selectObj.SetActive(false);
                optionObj.SetActive(false);
                cursorObj.SetActive(false);
                titleObj.transform.GetChild(0).GetComponent<ButtonUIController>().ResetCursor();
                break;
            case DispState.Select:
                titleObj.SetActive(false);
                selectObj.SetActive(true);
                optionObj.SetActive(false);
                cursorObj.SetActive(true);
                break;
            case DispState.Option:
                titleObj.SetActive(false);
                selectObj.SetActive(false);
                optionObj.SetActive(true);
                cursorObj.SetActive(false);
                optionObj.GetComponent<ButtonUIController>().ResetCursor();
                break;
        }
    }

    public void StartGame()
    {
        ChangeDisp(DispState.Select);
        sm.PlaySeByName("decide");
    }

    public void Option()
    {
        ChangeDisp(DispState.Option);
        sm.PlaySeByName("decide");
    }

    public void ChangeDispUI(int n)
    {
        ChangeDisp((DispState)n);
        sm.PlaySeByName("decide");
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
