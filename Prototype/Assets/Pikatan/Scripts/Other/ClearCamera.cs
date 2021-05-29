using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCamera : MonoBehaviour
{ 
    private float moveSpeed;
    [SerializeField]
    private float rotSpeed;
    private GameObject camObj;
    private Vector3 targetRot = new Vector3(0, 0, 0);
    private GameObject frontViewCamera;
    private bool isCalc = false;
    
    void Start()
    {
        camObj = GameObject.Find("CamObj");
        frontViewCamera = GameObject.Find("FrontViewCamera");
    }

    void Update()
    {
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            if(!isCalc)
            {
                moveSpeed = Mathf.Abs(camObj.transform.position.magnitude - Camera.main.transform.position.magnitude) * 2;
                rotSpeed = Mathf.Abs(camObj.transform.eulerAngles.magnitude - Camera.main.transform.eulerAngles.magnitude) * 2;
                isCalc = true;
            }
            frontViewCamera.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, camObj.transform.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRot), rotSpeed * Time.deltaTime);
        }
    }
}
