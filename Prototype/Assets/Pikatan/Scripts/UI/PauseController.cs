using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject defaultSelectButton;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(defaultSelectButton);
    }

    public void BeginPause()
    {
        EventSystem.current.SetSelectedGameObject(defaultSelectButton);
    }
}
