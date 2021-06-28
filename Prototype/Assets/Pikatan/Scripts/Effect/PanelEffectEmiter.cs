using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEffectEmiter : MonoBehaviour
{
    private GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        effect = Resources.Load("SelectEffect") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
