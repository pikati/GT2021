using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    private GameObject[] targetObjects;
    // Start is called before the first frame update
    void Start()
    {
        targetObjects = GameObject.FindGameObjectsWithTag("Panel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 SelectTargetPosition()
    {
        return Vector3.zero;
    }
}
