﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRevolution : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0, 0, speed * Time.deltaTime);
    }
}
