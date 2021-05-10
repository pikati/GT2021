using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public bool GetFlag { get; set; }
    private Renderer defaultRenderer;
    private Material defaultMat;
    private Material changeMat;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultRenderer = GetComponent<Renderer>();
        defaultMat = defaultRenderer.material;
        changeMat = new Material(defaultMat);
        changeMat.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetFlag == true)
        {
            defaultRenderer.material = changeMat;
        }
        else if (GetFlag == false)
        {
            defaultRenderer.material = defaultMat;
        }
    }

}
