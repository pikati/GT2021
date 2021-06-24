using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 titlePos;
    [SerializeField]
    private Vector3 selectPos;
    private TitleManager tm;
    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.Find("TitleManager").GetComponent<TitleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCam();
    }

    private void UpdateCam()
    {
        switch (tm.UIDispState)
        {
            case TitleManager.DispState.Start:
                MoveCam(titlePos);
                break;
            case TitleManager.DispState.Select:
                MoveCam(selectPos);
                break;
            default:
                break;
        }
    }

    private void MoveCam(Vector3 target)
    {

    }
}
