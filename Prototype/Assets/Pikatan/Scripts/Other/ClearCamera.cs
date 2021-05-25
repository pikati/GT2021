using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCamera : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotSpeed;
    private GameObject camObj;
    private Vector3 targetRot = new Vector3(0, 0, 0);
    private GameObject frontViewCamera;
    // Start is called before the first frame update
    void Start()
    {
        camObj = GameObject.Find("CamObj");
        frontViewCamera = GameObject.Find("FrontViewCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            frontViewCamera.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, camObj.transform.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRot), rotSpeed * Time.deltaTime);
        }
    }
}
