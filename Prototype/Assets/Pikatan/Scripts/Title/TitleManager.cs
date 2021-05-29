using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public enum DispState
    {
        Start,
        Title,
        Select,
        Option
    }

    private Fade fade;
    private GameObject startObj;
    private GameObject titleObj;
    private GameObject selectObj;
    private GameObject optionObj;
    private GameObject cursorObj;
    private DispState dispState;
    private SoundManager sm;
    private bool isStage = false;

    void Start()
    {
        fade = Singleton<Fade>.Instance;
        sm = Singleton<SoundManager>.Instance;
        startObj = GameObject.Find("StartUI");
        titleObj = GameObject.Find("TitleUI");
        selectObj = GameObject.Find("StageSelectUI");
        optionObj = GameObject.Find("OptionUI");
        cursorObj = GameObject.Find("SelectCursorUI");
        ChangeDisp(DispState.Start);
        sm.StopBgm();
        sm.PlayBgmByName("title");
        Invoke("FadeIn", 1.0f);
        if(isStage)
        {
            ChangeDisp(DispState.Select);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DispState.Start == dispState)
        {
            if (fade.isFading) return;
            if(Singleton<InputController>.Instance.A)
            {
                ChangeDisp(DispState.Title);
            }
        }
        if(DispState.Title != dispState && DispState.Start != dispState)
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
            case DispState.Start:
                startObj.SetActive(true);
                titleObj.SetActive(false);
                selectObj.SetActive(false);
                optionObj.SetActive(false);
                cursorObj.SetActive(false);
                break;
            case DispState.Title:
                startObj.SetActive(false);
                titleObj.SetActive(true);
                selectObj.SetActive(false);
                optionObj.SetActive(false);
                cursorObj.SetActive(false);
                titleObj.transform.GetChild(0).GetComponent<ButtonUIController>().ResetCursor();
                break;
            case DispState.Select:
                startObj.SetActive(false);
                titleObj.SetActive(false);
                selectObj.SetActive(true);
                optionObj.SetActive(false);
                cursorObj.SetActive(true);
                Singleton<StageIconController>.Instance.SetIcon();
                break;
            case DispState.Option:
                startObj.SetActive(false);
                titleObj.SetActive(false);
                selectObj.SetActive(false);
                optionObj.SetActive(true);
                cursorObj.SetActive(false);
                optionObj.GetComponent<ButtonUIController>().ResetCursor();
                break;
        }
    }

    private void FadeIn()
    {
        fade.FadeOut();
    }

    public void DispStageSelect()
    {
        isStage = true;
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
