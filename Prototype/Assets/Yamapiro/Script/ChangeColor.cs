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

    public AxisState AState { get; set; } = AxisState.Non;
    private bool isFocus = false;
    private Renderer defaultRenderer;
    private Material defaultMat;
    private Material changeMat;
    private Material focusMat;
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

    public void ChangeAxisState(AxisState state)
    {
        AState = state;
    }
}
