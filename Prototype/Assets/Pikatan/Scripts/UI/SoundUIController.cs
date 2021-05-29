using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUIController : MonoBehaviour
{
    private SoundManager sm;
    [SerializeField]
    private ButtonUIController bc;
    [SerializeField]
    private GameObject masterVolumeObj;
    [SerializeField]
    private GameObject bgmVolumeObj;
    [SerializeField]
    private GameObject seVolumeObj;
    private InputController ic;
    private GameObject[] masterVolumeObjs = new GameObject[10];
    private GameObject[] bgmVolumeObjs = new GameObject[10];
    private GameObject[] seVolumeObjs = new GameObject[10];
    private readonly int maxVolume = 10;
    private bool isInput = false;

    void Start()
    {
        sm = Singleton<SoundManager>.Instance;
        ic = Singleton<InputController>.Instance;
        for (int i = 0; i < 10; i++)
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
        if (ic.MoveValue.x < -0.7f || ic.ArrowValue.x < 0)
        {
            if (isInput) return;
            isInput = true;
            if (bc.ButtonIdx == 0)
            {
                sm.MasterVolumeProperty -= 0.1f;
                sm.MasterLevel--;
                if (sm.MasterLevel < 0)
                {
                    sm.MasterLevel = 0;
                }
                Singleton<SoundManager>.Instance.PlaySeByName("decide");
            }
            else if (bc.ButtonIdx == 1)
            {
                sm.BgmVolumeProperty -= 0.1f;
                sm.BGMLevel--;
                if (sm.BGMLevel < 0)
                {
                    sm.BGMLevel = 0;
                }
                Singleton<SoundManager>.Instance.PlaySeByName("decide");
            }
            else if (bc.ButtonIdx == 2)
            {
                sm.SeVolumeProperty -= 0.1f;
                sm.SELevel--;
                if (sm.SELevel < 0)
                {
                    sm.SELevel = 0;
                }
                Singleton<SoundManager>.Instance.PlaySeByName("decide");
            }
        }
        else if (ic.MoveValue.x > 0.7f || ic.ArrowValue.x > 0)
        {
            if (isInput) return;
            isInput = true;
            if (bc.ButtonIdx == 0)
            {
                sm.MasterVolumeProperty += 0.1f;
                sm.MasterLevel++;
                if (sm.MasterLevel > maxVolume)
                {
                    sm.MasterLevel = maxVolume;
                }
                Singleton<SoundManager>.Instance.PlaySeByName("decide");
            }
            else if (bc.ButtonIdx == 1)
            {
                sm.BgmVolumeProperty += 0.1f;
                sm.BGMLevel++;
                if (sm.BGMLevel > maxVolume)
                {
                    sm.BGMLevel = maxVolume;
                }
                Singleton<SoundManager>.Instance.PlaySeByName("decide");
            }
            else if (bc.ButtonIdx == 2)
            {
                sm.SeVolumeProperty += 0.1f;
                sm.SELevel++;
                if (sm.SELevel > maxVolume)
                {
                    sm.SELevel = maxVolume;
                }
                Singleton<SoundManager>.Instance.PlaySeByName("decide");
            }
        }
        else
        {
            isInput = false;
        }
    }

    private void DispVolume()
    {
        for (int i = 0; i < sm.MasterLevel; i++)
        {
            masterVolumeObjs[i].SetActive(true);
        }
        for (int i = sm.MasterLevel; i < maxVolume; i++)
        {
            masterVolumeObjs[i].SetActive(false);
        }

        for (int i = 0; i < sm.BGMLevel; i++)
        {
            bgmVolumeObjs[i].SetActive(true);
        }
        for (int i = sm.BGMLevel; i < maxVolume; i++)
        {
            bgmVolumeObjs[i].SetActive(false);
        }

        for (int i = 0; i < sm.SELevel; i++)
        {
            seVolumeObjs[i].SetActive(true);
        }
        for (int i = sm.SELevel; i < maxVolume; i++)
        {
            seVolumeObjs[i].SetActive(false);
        }
    }
}
