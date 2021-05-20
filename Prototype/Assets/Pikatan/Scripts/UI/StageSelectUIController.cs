﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUIController : MonoBehaviour
{
    [SerializeField]
    private int maxPage;
    [SerializeField]
    private float pageChangeSpeed;
    [SerializeField]
    private List<GameObject> cursors;
    private InputController ic;
    private bool isInput = false;
    private int index = 0;
    private int page = 0;
    private StageName stageName;
    private float diff = 0;
    private bool isChangePage = false;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 10; i++)
        {
            DisableCursor(i);
        }
        ic = Singleton<InputController>.Instance;
        stageName = new StageName();
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ic.MoveValue.x > 0 || ic.ArrowValue.x > 0)
        {
            if (isInput) return;
            ChangeCursor(1);
        }
        else if (ic.MoveValue.x < 0 || ic.ArrowValue.x < 0)
        {
            if (isInput) return;
            ChangeCursor(-1);
        }
        else if (ic.MoveValue.y > 0 || ic.ArrowValue.y > 0)
        {
            if (isInput) return;
            ChangeCursor(-5);
        }
        else if (ic.MoveValue.y < 0 || ic.ArrowValue.y < 0)
        {
            if (isInput) return;
            ChangeCursor(5);
        }
        else if (ic.RB)
        {
            if (isInput) return;
            ChangePage(1);
        }
        else if (ic.LB)
        {
            if (isInput) return;
            ChangePage(-1);
        }
        else
        {
            isInput = false;
        }
        if (ic.A)
        {
            ChangeSecne(stageName.StageNames[index + page]);
        }
        MovePage();
    }

    private void ChangeCursor(int n)
    {
        DisableCursor(index);
        int tmp = index;
        index += n;
        if (index < 0)
        {
            index = tmp;
        }
        else if (index >= 10)
        {
            index = tmp;
        }
        else
        {
            Singleton<SoundManager>.Instance.PlaySeByName("ok_no9");
        }
        EnableCursor(index);
        isInput = true;
    }

    private void ChangePage(int n)
    {
        int prePage = page;
        page += n;
        if(page < 0)
        {
            page = 0;
        }
        if(page > maxPage)
        {
            page = maxPage;
        }
        if(page > prePage)
        {
            diff += -2000.0f;
            isChangePage = true;
        }
        else if(page < prePage)
        {
            diff += 2000.0f;
            isChangePage = true;
        }
        isInput = true;
    }

    private void MovePage()
    {
        if (!isChangePage) return;
        Vector3 pos = rt.localPosition;
        pos.x = diff;
        rt.localPosition = Vector3.Lerp(rt.localPosition, pos, Time.deltaTime * pageChangeSpeed);
        if(Mathf.Abs(transform.position.x - pos.x) < 0.01f)
        {
            isChangePage = false;
        }
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
        index = 0;
        EnableCursor(0);
        for (int i = 1; i < 10; i++)
        {
            DisableCursor(i);
        }
    }

    private void ChangeSecne(string sceneName)
    {
        Singleton<SceneChanger>.Instance.SceneChange(sceneName);
    }
}
