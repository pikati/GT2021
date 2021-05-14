using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : Singleton<CameraRotater>
{
    private enum CameraState
    {
        NoRotate,
        Rotating
    }

    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float endRot = 80.0f;
    [SerializeField]
    private float endPos = 1;
    private float startRot;
    private float startPos;
    private GameObject camera1;
    private GameObject camera2;
    private CameraState state = CameraState.NoRotate;
    private bool isBeginRotate = false;
    private bool isEndRotate = false;
    private float targetRot;
    private float targetPos;


    private void Start()
    {
        camera1 = Camera.main.gameObject;
        camera2 = GameObject.Find("FrontViewCamera");
        startRot = camera1.transform.eulerAngles.x;
        startPos = camera1.transform.position.z;
    }

    private void Update()
    {
        if (state == CameraState.NoRotate) return;

        float newAngle = Mathf.MoveTowards(camera1.transform.eulerAngles.x, targetRot, Time.deltaTime * rotateSpeed);
        camera1.transform.eulerAngles = new Vector3(newAngle, 0, 0);
        camera2.transform.eulerAngles = new Vector3(newAngle, 0, 0);

        float newPoint = Mathf.MoveTowards(camera1.transform.position.z, targetPos, Time.deltaTime * moveSpeed);
        Vector3 pos = new Vector3(camera1.transform.position.x, camera1.transform.position.y, newPoint);
        camera1.transform.position = pos;
        camera2.transform.position = pos;
        Debug.Log(pos.z);

        CheckeEndRotate();
    }

    public void EndRotate()
    {
        state = CameraState.Rotating;
    }


    public void CameraRotate()
    {
        if (state == CameraState.NoRotate)
        {
            targetRot = endRot;
            targetPos = endPos;
            state = CameraState.Rotating;
            isBeginRotate = true;
            isEndRotate = false;
        }
    }

    private void CheckeEndRotate()
    {
        if (isBeginRotate)
        {
            if (Mathf.Abs(camera1.transform.eulerAngles.x - targetRot) < 0.01f)
            {
                state = CameraState.NoRotate;
                if (isEndRotate)
                {
                    isBeginRotate = false;
                    
                }
                targetRot = startRot;
                targetPos = startPos;
                isEndRotate = true;
            }
        }
        else
        {
            state = CameraState.NoRotate;
        }
    }

    private void Reset()
    {
        
    }
}
