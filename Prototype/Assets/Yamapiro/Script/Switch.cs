using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject Panel;

    private GameObject[] SwitchObj;

    private List<Switch> Switches = new List<Switch>();

    private bool is_On = false;

    // Start is called before the first frame update
    void Start()
    {
        SwitchObj = GameObject.FindGameObjectsWithTag("Switch");
        foreach (GameObject obj in SwitchObj)
        {
            Switches.Add(obj.GetComponent<Switch>());
        }
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
                    sw.ChangeSwitchState(false, gameObject.name);
                }
            }
            else
            {
                //falseになってパネルが表示される
                ChangeSwitchState(false);
                foreach (Switch sw in Switches)
                {
                    sw.ChangeSwitchState(false, gameObject.name);
                }
            }
        }
    }

    private void ChangeSwitchState(bool is_On, string name = "non")
    {
        if (name == gameObject.name) return;
        Debug.Log(gameObject.name + " 通った");
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
