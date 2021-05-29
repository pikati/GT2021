using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCamera : MonoBehaviour
{ 
    private GameObject camObj;
    private Vector3 targetRot = new Vector3(0, 0, 0);
    private GameObject frontViewCamera;
    private bool isCalc = false;
    private float time = 1.0f;
    private float startTime;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool isMoveEnd = false;
    
    void Start()
    {
        camObj = GameObject.Find("CamObj");
        frontViewCamera = GameObject.Find("FrontViewCamera");
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        if (isMoveEnd) return;
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            if(!isCalc)
            {
                startTime = Time.timeSinceLevelLoad;
                frontViewCamera.SetActive(false);
                isCalc = true;
            }
            float diff = Time.timeSinceLevelLoad - startTime;
            if(diff > time)
            {
                transform.position = camObj.transform.position;
                transform.rotation = Quaternion.Euler(targetRot);
                isMoveEnd = true;
            }
            var rate = diff / time;
            transform.position = Vector3.Lerp(startPos, camObj.transform.position, rate);
            transform.rotation = Quaternion.Lerp(startRot, Quaternion.Euler(targetRot), rate);
        }
    }
}
