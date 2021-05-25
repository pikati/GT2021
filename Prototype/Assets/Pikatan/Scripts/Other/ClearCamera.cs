﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCamera : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotSpeed;
    private GameObject camObj;
    private Vector3 targetRot = new Vector3(120, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        camObj = GameObject.Find("CamObj");
    }

    // Update is called once per frame
    void Update()
    {
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            transform.position = Vector3.MoveTowards(transform.position, camObj.transform.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 90f, 0), rotSpeed * Time.deltaTime);
        }
    }
}
