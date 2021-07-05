using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public enum AxisState
    {
        Non,
        Forcus,
        OnPlayer
    };

    private enum DefaultState 
    { 
        ToWhite,
        ToYellow
    }


    public AxisState AState { get; set; } = AxisState.Non;
    private bool isFocus = false;
    private Renderer defaultRenderer;
    private Material defaultMat;
    private Material changeMat;
    private Material focusMat;
    private DefaultState DState = DefaultState.ToWhite;
    private GameTimer defaultColorTimer = new GameTimer(1.0f);
    //[SerializeField]
    //private GameObject a;
    
    void Start()
    {
        defaultRenderer = GetComponent<Renderer>();
        defaultMat = defaultRenderer.material;
        changeMat = new Material(defaultMat);
        changeMat.color = Color.red;
        focusMat = new Material(defaultMat);
        focusMat.color = Color.blue;
    }

    void Update()
    {
        switch (AState)
        {
            case AxisState.OnPlayer:
                defaultRenderer.material = changeMat;
                break;
            case AxisState.Non:
                ChangeDefaultColor();
                defaultRenderer.material = defaultMat;
                break;
            case AxisState.Forcus:
                defaultRenderer.material = focusMat;
                break;
            default:
                break;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("AxisPointer"))
        {
            ChangeState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AxisPointer"))
        {
            ChangeState(false);
        }
    }

    private void ChangeState(bool state)
    {
        if(state == true)
        {
            if (AState == AxisState.OnPlayer) return;
            ChangeAxisState(AxisState.Forcus);
        }
        else
        {
            ChangeAxisState(AxisState.Non);
        }
        //a.SetActive(isFocus);
    }

    private void ChangeDefaultColor()
    {
        if(defaultColorTimer.UpdateTimer())
        {
            if(DState == DefaultState.ToWhite)
            {
                DState = DefaultState.ToYellow;
            }
            else
            {
                DState = DefaultState.ToWhite;
            }
            defaultColorTimer.ResetTimer(1.0f);
        }
        switch (DState)
        {
            case DefaultState.ToWhite:
                defaultMat.color = Color.Lerp(defaultMat.color, Color.white, Time.deltaTime * 2);
                break;
            case DefaultState.ToYellow:
                defaultMat.color = Color.Lerp(defaultMat.color, Color.yellow, Time.deltaTime * 2);
                break;
            default:
                break;
        }
        

    }

    public void ChangeAxisState(AxisState state)
    {
        AState = state;
    }
}
