using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 titlePos;
    [SerializeField]
    private Vector3 selectPos;
    [SerializeField]
    private float speed = 1.0f;
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
        if (!tm.IsStartFade) return;
        switch (tm.UIDispState)
        {
            case TitleManager.DispState.Start:
                MoveCam(titlePos);
                break;
            case TitleManager.DispState.Title:
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
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
