using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject Panel;

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
            //trueになったらスイッチがオンになり赤になる
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            //falseになったらスイッチがオフになり青になる
            GetComponent<Renderer>().material.color = Color.blue;
  
        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!is_On)
            {
                //trueになってパネルが非表示になる
                is_On = true;
                Panel.SetActive(false);
            }
            else
            {
                //falseになってパネルが表示される
                is_On = false;
                Panel.SetActive(true);
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (is_On == true)
    //    {

    //    }
    //}
}
