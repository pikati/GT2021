using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    bool GetFlag { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GetFlag == true)
        {
            //色を赤に変更
            GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("選択されているよ");
        }
        else if (GetFlag == false)
        {
            Debug.Log("選択されてないよ");
            //色を白に変更
            GetComponent<Renderer>().material.color = Color.white;

        }
    }

}
