using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //ROtatePointのupdateコメントしてるｐ
    public bool GetFlag { get; set; }
    private bool isFocus = false;
    private Renderer defaultRenderer;
    private Material defaultMat;
    private Material changeMat;
    private Material focusMat;
    [SerializeField]
    private GameObject a;
    
    void Start()
    {
        defaultRenderer = GetComponent<Renderer>();
        defaultMat = defaultRenderer.material;
        changeMat = new Material(defaultMat);
        changeMat.color = Color.red;
        focusMat = new Material(defaultMat);
        focusMat.color = Color.yellow;
    }

    void Update()
    {
        if (GetFlag == true)
        {
            defaultRenderer.material = changeMat;
        }
        else if (GetFlag == false)
        {
            if(isFocus)
            {
                defaultRenderer.material = focusMat;
            }
            defaultRenderer.material = defaultMat;
        }
    }


    private void OnTriggerEnter(Collider other)
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
        isFocus = state;
        a.SetActive(isFocus);
    }
}
