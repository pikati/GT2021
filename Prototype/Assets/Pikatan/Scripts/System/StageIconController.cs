using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageIconController : Singleton<StageIconController>
{
    [SerializeField]
    private Image[] images;
    [SerializeField]
    private Sprite[] icons;
    private StageClearManager scm;

    void Start()
    {
        scm = Singleton<StageClearManager>.Instance;
    }

    public void SetIcon()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = icons[(int)scm.GetClearState(i)];
        }
    }
}
