using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Singleton<SoundManager>.Instance.PlayBgmByName("cocoro");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            Singleton<SoundManager>.Instance.PlaySeByName("ok_no9");
            Debug.Log("SE１再生！");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Singleton<SoundManager>.Instance.PlaySeByName("ok_no10");
            Debug.Log("SE２再生！");
        }

        //マスター調整
        if(Input.GetKey(KeyCode.I))
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                Singleton<SoundManager>.Instance.MasterVolumeProperty -= 0.05f;
            }
            else if(Input.GetKeyDown(KeyCode.L))
            {
                Singleton<SoundManager>.Instance.MasterVolumeProperty += 0.05f;
            }
        }

        if (Input.GetKey(KeyCode.O))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Singleton<SoundManager>.Instance.BgmVolumeProperty -= 0.05f;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Singleton<SoundManager>.Instance.BgmVolumeProperty += 0.05f;
            }
        }

        if (Input.GetKey(KeyCode.P))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Singleton<SoundManager>.Instance.SeVolumeProperty -= 0.05f;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Singleton<SoundManager>.Instance.SeVolumeProperty += 0.05f;
            }
        }
    }
}
