using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointWeight : MonoBehaviour
{
    private Weight up = Weight.No;
    private Weight down = Weight.No;
    private Weight left = Weight.No;
    private Weight right = Weight.No;
    public enum Weight
    {
        No,
        In,
        Out,
        InOut,
        NoObj
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
