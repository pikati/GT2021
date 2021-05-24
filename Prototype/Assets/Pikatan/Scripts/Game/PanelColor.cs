using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelColor : MonoBehaviour
{
    private List<Material> panels = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Panel");
        foreach (GameObject obj in objs)
        {
            panels.Add(obj.GetComponent<MeshRenderer>().material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
