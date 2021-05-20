using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUIController : MonoBehaviour
{
    private SoundManager sm;
    [SerializeField]
    private ButtonUIController bc;
    private InputController ic;
    private GameObject[] masterVolumeObjs = new GameObject[10];
    private GameObject[] bgmVolumeObjs = new GameObject[10];
    private GameObject[] seVolumeObjs = new GameObject[10];
    private readonly int maxVolume = 10;
    private int master = 5;
    private int bgm = 10;
    private int se = 10;
    private bool isInput = false;

    void Start()
    {
        sm = Singleton<SoundManager>.Instance;
        ic = Singleton<InputController>.Instance;
        GameObject masterVolumeObj = GameObject.Find("Master");
        GameObject bgmVolumeObj = GameObject.Find("BGM");
        GameObject seVolumeObj = GameObject.Find("SE");
        for(int i = 0; i < 10; i++)
        {
            masterVolumeObjs[i] = masterVolumeObj.transform.GetChild(i).gameObject;
            bgmVolumeObjs[i] = bgmVolumeObj.transform.GetChild(i).gameObject;
            seVolumeObjs[i] = seVolumeObj.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        ChangeSound();
        DispVolume();
    }

    private void ChangeSound()
    {
        if (ic.MoveValue.x < 0 || ic.ArrowValue.x < 0)
        {
            if (isInput) return;
            isInput = true;
            if (bc.ButtonIdx == 0)
            {
                sm.MasterVolumeProperty -= 0.1f;
                master--;
                if(master < 0)
                {
                    master = 0;
                }
            }
            else if (bc.ButtonIdx == 1)
            {
                sm.BgmVolumeProperty -= 0.1f;
                bgm--;
                if(bgm < 0)
                {
                    bgm = 0;
                }
            }
            else if (bc.ButtonIdx == 2)
            {
                sm.SeVolumeProperty -= 0.1f;
                se--;
                if(se < 0)
                {
                    se = 0;
                }
            }
        }
        else if (ic.MoveValue.x > 0 || ic.ArrowValue.x > 0)
        {
            if (isInput) return;
            isInput = true;
            if (bc.ButtonIdx == 0)
            {
                sm.MasterVolumeProperty += 0.1f;
                master++;
                if(master > maxVolume)
                {
                    master = maxVolume;
                }
            }
            else if (bc.ButtonIdx == 1)
            {
                sm.BgmVolumeProperty += 0.1f;
                bgm++;
                if(bgm > maxVolume)
                {
                    bgm = maxVolume;
                }
            }
            else if (bc.ButtonIdx == 2)
            {
                sm.SeVolumeProperty += 0.1f;
                se++;
                if(se > maxVolume)
                {
                    se = maxVolume;
                }
            }
        }
        else
        {
            isInput = false;
        }
    }

    private void DispVolume()
    {
        for(int i = 0; i < master; i++)
        {
            masterVolumeObjs[i].SetActive(true);
        }
        for(int i = master; i < maxVolume; i++)
        {
            masterVolumeObjs[i].SetActive(false);
        }

        for (int i = 0; i < bgm; i++)
        {
            bgmVolumeObjs[i].SetActive(true);
        }
        for (int i = bgm; i < maxVolume; i++)
        {
            bgmVolumeObjs[i].SetActive(false);
        }

        for (int i = 0; i < se; i++)
        {
            seVolumeObjs[i].SetActive(true);
        }
        for (int i = se; i < maxVolume; i++)
        {
            seVolumeObjs[i].SetActive(false);
        }
    }
}
