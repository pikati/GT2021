using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButtonSelector : Singleton<DefaultButtonSelector>
{
    [SerializeField]
    private GameObject titleSelect;
    [SerializeField]
    private GameObject selectSelect;
    [SerializeField]
    private GameObject optionSelect;
    // Start is called before the first frame update
    void Start()
    {
        SelectButton(TitleManager.DispState.Title);
    }

    public void SelectButton(TitleManager.DispState state)
    {
        switch (state)
        {
            case TitleManager.DispState.Title:
                EventSystem.current.SetSelectedGameObject(titleSelect);
                break;
            case TitleManager.DispState.Select:
                EventSystem.current.SetSelectedGameObject(selectSelect);
                break;
            case TitleManager.DispState.Option:
                EventSystem.current.SetSelectedGameObject(optionSelect);
                break;
        }

    }
}
