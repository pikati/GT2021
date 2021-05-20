using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonUIController : MonoBehaviour
{
    [SerializeField]
    private int buttonNum;
    [SerializeField]
    private UnityEvent[] buttonEvent;
    private List<GameObject> cursors = new List<GameObject>();
    private InputController ic;
    private bool isInput = false;
    public int ButtonIdx { get; private set; } = 0;
    void Start()
    {
        for(int i = 0; i < buttonNum; i++)
        {
            cursors.Add(transform.GetChild(i).gameObject);
        }
        for(int i = 1; i < buttonNum; i++)
        {
            DisableCursor(i);
        }
        ic = Singleton<InputController>.Instance;
    }

    void Update()
    {
        if(ic.MoveValue.y > 0 || ic.ArrowValue.y > 0)
        {
            if (isInput) return;
            ChangeCursor(-1);
        }
        else if(ic.MoveValue.y < 0 || ic.ArrowValue.y < 0)
        {
            if (isInput) return;
            ChangeCursor(1);
        }
        else
        {
            isInput = false;
        }
        if(ic.A)
        {
            buttonEvent[ButtonIdx].Invoke();
        }
    }

    private void ChangeCursor(int n)
    {
        DisableCursor(ButtonIdx);
        ButtonIdx += n;
        if(ButtonIdx < 0)
        {
            ButtonIdx = 0;
        }
        else if(ButtonIdx >= cursors.Count)
        {
            ButtonIdx = cursors.Count - 1;
        }
        else
        {
            Singleton<SoundManager>.Instance.PlaySeByName("ok_no9");
        }
        EnableCursor(ButtonIdx);
        isInput = true;
    }

    private void EnableCursor(int index)
    {
        cursors[index].SetActive(true);
        cursors[index].GetComponent<CursorAnimation>().StartAniamtion();
    }

    private void DisableCursor(int index)
    {
        cursors[index].SetActive(false);
    }

    public void ResetCursor()
    {
        ButtonIdx = 0;
        EnableCursor(0);
        for (int i = 1; i < buttonNum; i++)
        {
            DisableCursor(i);
        }
    }
}
