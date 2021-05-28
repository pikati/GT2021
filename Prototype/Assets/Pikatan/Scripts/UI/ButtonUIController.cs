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
    private bool isInput = true;
    private SoundManager sm;
    public int ButtonIdx { get; private set; } = 0;
    void Awake()
    {
        for(int i = 1; i < buttonNum + 1; i++)
        {
            cursors.Add(transform.GetChild(i).gameObject);
        }
        for(int i = 1; i < buttonNum; i++)
        {
            DisableCursor(i);
        }
        ic = Singleton<InputController>.Instance;
        sm = Singleton<SoundManager>.Instance;
        ButtonIdx = 0;
    }

    void Update()
    {
        if(ic.MoveValue.y > 0.7f || ic.ArrowValue.y > 0)
        {
            if (isInput) return;
            ChangeCursor(-1);
        }
        else if(ic.MoveValue.y < -0.7f || ic.ArrowValue.y < 0)
        {
            if (isInput) return;
            ChangeCursor(1);
        }
        else if(ic.A)
        {
            if (isInput) return;
            isInput = true;
            buttonEvent[ButtonIdx].Invoke();
        }
        else
        {
            isInput = false;
        }
    }

    private void ChangeCursor(int n)
    {
        isInput = true;
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
            sm.PlaySeByName("cursor");
        }
        EnableCursor(ButtonIdx);
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
