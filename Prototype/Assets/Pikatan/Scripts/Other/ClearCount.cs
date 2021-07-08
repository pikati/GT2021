using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCount : Singleton<ClearCount>
{
    //常時出し続ける仕様変更のため
    //public enum ImageState
    //{
    //    Visible,
    //    Fade,
    //    InVisble
    //};

    private List<GameObject> goalImages = new List<GameObject>(5);
    private List<Image> spriteImage;
    private int goalCount = 0;
    //private readonly float eraseTime = 2.0f;
    //private readonly float fadeTime = 5.0f;
    //private GameTimer timer;
    //private ImageState state = ImageState.Visible;
    private int clearNum;
    private Vector3[] goalImagePosition;
    // Start is called before the first frame update
    void Start()
    {
        clearNum = Singleton<ClearChecker>.Instance.ClearNum;
        goalImages.Add(transform.GetChild(0).gameObject);
        goalImages.Add(transform.GetChild(1).gameObject);
        goalImages.Add(transform.GetChild(2).gameObject);
        goalImages.Add(transform.GetChild(3).gameObject);
        goalImages.Add(transform.GetChild(4).gameObject);
        spriteImage = new List<Image>(clearNum * 2);
        for(int i = 0; i < clearNum; i++)
        {
            spriteImage.Add(transform.GetChild(i).transform.GetChild(0).GetComponent<Image>());
            spriteImage.Add(transform.GetChild(i).transform.GetChild(1).GetComponent<Image>());
        }
        for(int i = clearNum; i < 5; i++)
        {
            goalImages[i].SetActive(false);
        }
        goalImagePosition = new Vector3[5];
        goalImagePosition[0] = new Vector3(0, 1, 5);
        goalImagePosition[1] = new Vector3(0.08f, 1, 5);
        goalImagePosition[2] = new Vector3(0.16f, 1, 5);
        goalImagePosition[3] = new Vector3(0.24f, 1, 5);
        goalImagePosition[4] = new Vector3(0.32f, 1, 5);

        //timer = new GameTimer(eraseTime);
    }

    private void Update()
    {
        //if (state == ImageState.InVisble) return;
        //if(timer.UpdateTimer())
        //{
        //    if (state == ImageState.Visible)
        //    {
        //        ChangeState(ImageState.Fade);
        //    }
        //    else if(state == ImageState.Fade)
        //    {
        //        ChangeState(ImageState.InVisble);
        //    }
        //}
        
        //if(state == ImageState.Fade)
        //{
        //    SetAlpha(1.0f - timer.ElaspedTime);
        //}
    }

    //private void SetAlpha(float a)
    //{
    //    for(int i = 0; i < clearNum * 2; i++)
    //    {
    //        Color color = spriteImage[i].color;
    //        color.a = a;
    //        spriteImage[i].color = color;
    //    }
    //}

    //public void ChangeState(ImageState state)
    //{
    //    this.state = state;
    //    if(state == ImageState.Visible)
    //    {
    //        timer.ResetTimer(eraseTime);
    //        SetAlpha(1);
    //    }
    //    else if(state == ImageState.Fade)
    //    {
    //        timer.ResetTimer(fadeTime);
    //    }
    //    else if(state == ImageState.InVisble)
    //    {
    //        SetAlpha(0); 
    //    }
    //}

    public void UpdateClearNum()
    {
        //ゴールの画像オブジェの子供にあるMaskオブジェを取得しそれを非表示に
        goalImages[goalCount++].transform.GetChild(1).gameObject.SetActive(false);
    }

    public Vector3 GetActiveIconPosition()
    {
        return goalImagePosition[goalCount];
    }
}
