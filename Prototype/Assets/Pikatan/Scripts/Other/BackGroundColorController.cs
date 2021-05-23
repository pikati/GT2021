using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundColorController : MonoBehaviour
{
    private enum BGState
    { 
        Top,
        Bottom
    }
    private Material skyboxMat;
    [SerializeField]
    private Color topColor;
    [SerializeField]
    private Color bottomColor;
    [SerializeField]
    private float gradationTime = 5.0f;
    private Color bgTopColor;
    private Color bgBottomColor;
    private GameTimer bgTimer;
    private BGState state = BGState.Bottom;
    // Start is called before the first frame update
    void Start()
    {
        skyboxMat = RenderSettings.skybox;
        bgTimer = new GameTimer(gradationTime);
        bgTopColor = topColor;
        bgBottomColor = bottomColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (bgTimer.UpdateTimer())
        {
            ChangeState();
            bgTimer.ResetTimer(gradationTime);
        }
        UpdateColor();
        if (skyboxMat.HasProperty("_TopColor"))
        {
            skyboxMat.SetColor("_TopColor", bgTopColor);
        }
        if (skyboxMat.HasProperty("_BottomColor"))
        {
            skyboxMat.SetColor("_BottomColor", bgBottomColor);
        }
        Debug.Log(bgTopColor);
    }

    private void ChangeState()
    {
        if(state == BGState.Bottom)
        {
            state = BGState.Top;
        }
        else
        {
            state = BGState.Bottom;
        }
    }

    private void UpdateColor()
    {
        if (state == BGState.Bottom)
        {
            bgTopColor = Color.Lerp(bgTopColor, bottomColor, Time.deltaTime / gradationTime);
            bgBottomColor = Color.Lerp(bgBottomColor, topColor, Time.deltaTime / gradationTime);
        }
        else
        {
            bgTopColor = Color.Lerp(bgTopColor, topColor, Time.deltaTime / gradationTime);
            bgBottomColor = Color.Lerp(bgBottomColor, bottomColor, Time.deltaTime / gradationTime);
        }
    }
}
