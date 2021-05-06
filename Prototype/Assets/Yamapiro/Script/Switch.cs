using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject Panel;

    public GameObject[] SwitchObj;

    private List<Switch> Switches = new List<Switch>();

    public bool is_On;

    // Start is called before the first frame update
    void Start()
    {
        SwitchObj = GameObject.FindGameObjectsWithTag("Switch");
        foreach (GameObject obj in SwitchObj)
        {
            Switches.Add(obj.GetComponent<Switch>());
        }
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!is_On)
            {
                //trueになってパネルが非表示になる
                ChangeSwitchState(true);
                foreach(Switch sw in Switches)
                {
                    sw.ChangeSwitchState(false);
                }
            }
            else
            {
                //falseになってパネルが表示される
                ChangeSwitchState(false);
                foreach (Switch sw in Switches)
                {
                    sw.ChangeSwitchState(true);
                }
            }
        }
    }

    private void ChangeSwitchState(bool is_On)
    {
        this.is_On = is_On;
        if (is_On)
        {
            //trueになったらスイッチがオンになり赤になる
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            //falseになったらスイッチがオフになり青になる
            GetComponent<Renderer>().material.color = Color.blue;
        }

        PanelSetActive();
    }

    private void PanelSetActive()
    {
        Panel.SetActive(!is_On);
    }
}
