using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool is_On;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(is_On)
        {
            //trueになったら赤になる
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!is_On)
        {
            is_On = true;
        }
        else
        {
            is_On = false;
        }
    }
}
