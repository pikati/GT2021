using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePoint : MonoBehaviour
{
    public enum RotateState 
    { 
        NoRotate,
        Rotated
    }

    public enum RotateAxis
    {
        X,
        Y,
        Z
    }
    [SerializeField]
    private bool isActive = false;
    public bool IsActive { get; set; }

    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private RotateAxis rotateAxis = RotateAxis.Z;
    [SerializeField]
    private GameObject upObj = null;
    [SerializeField]
    private GameObject downObj = null;
    [SerializeField]
    private GameObject leftObj = null;
    [SerializeField]
    private GameObject rightObj = null;
    private RotateState rotateState = RotateState.NoRotate;
    private bool isRotate = false;
    private float rotateValue;
    private float[] angles = new float[3];
    private ChangeColor changeColor;
    private AreaChilder[] areaChilders = new AreaChilder[2];

    public GameObject[] selectableObjs { get; private set; }
    public bool OnPlayer { get; set; } = false;

    private void Start()
    {
        IsActive = isActive;
        SetRotateValue();
        changeColor =GetComponentInChildren<ChangeColor>();
        areaChilders[0] = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<AreaChilder>();
        areaChilders[1] = transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<AreaChilder>();
        selectableObjs = new GameObject[4];
        selectableObjs[0] = upObj;
        selectableObjs[1] = downObj;
        selectableObjs[2] = leftObj;
        selectableObjs[3] = rightObj;

    }


    // Update is called once per frame
    void Update()
    {
        if(Singleton<StageState>.Instance.NowStageState == StageState.StageStateEnum.Rotate)
        {
            changeColor.GetFlag = IsActive;
        }
        else
        {
            changeColor.GetFlag = false;
        }
        if (isRotate)
        {
            float deg = rotateState == RotateState.NoRotate ? 180.0f : 0;
            //transform.Rotate(rotateValue * rotateSpeed * Time.deltaTime);
            float angleSpeed = rotateValue * rotateSpeed * Time.deltaTime;
            angles[(int)rotateAxis] += angleSpeed;
            transform.rotation = transform.rotation * Quaternion.AngleAxis(angleSpeed, GetAxis());
            if (IsRotateComplete(angles[(int)rotateAxis], deg))
            {
                angles[(int)rotateAxis] = deg;
                if (rotateState == RotateState.NoRotate)
                {
                    rotateState = RotateState.Rotated;
                }
                else
                {
                    rotateState = RotateState.NoRotate;
                }
                Vector3 ang = transform.rotation.eulerAngles;
                ang.z = deg;
                transform.rotation = Quaternion.Euler(ang);
                Singleton<NavMeshBaker>.Instance.Bake();
            }
        }
        Debug.Log(OnPlayer);
    }

    public void BeginRotate()
    {
        if (!IsActive) return;
        if (OnPlayer) return; 
        Invoke("IsRotateTure", 0.05f);
        SetRotateValue();
        areaChilders[0].IsActive = true;
        areaChilders[1].IsActive = true;
    }

    //初期化と回転フラグ立った時に呼べ
    private void SetRotateValue()
    {
        //switch (rotateAxis)
        //{
        //    case RotateAxis.X:
        //        rotateValue = rotateState == RotateState.NoRotate ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        //        break;
        //    case RotateAxis.Y:
        //        rotateValue = rotateState == RotateState.NoRotate ? new Vector3(0, 1, 0) : new Vector3(0, -1, 0);
        //        break;
        //    case RotateAxis.Z:
        //        rotateValue = rotateState == RotateState.NoRotate ? new Vector3(0, 0, 1) : new Vector3(0, 0, -1);
        //        break;
        //    default:
        //        Debug.LogError("回転軸おかしくてワロタ");
        //        break;
        //}
        rotateValue = rotateState == RotateState.NoRotate ? 1 : -1;
    }

    private bool IsRotateComplete(float angle, float deg)
    {
        
        if (rotateState == RotateState.NoRotate)
        {
            //回転していない状態（デフォルトの状態）なので指定された角度以上ｔるえ
            if(angle >= deg)
            {
                isRotate = false;
                areaChilders[0].IsActive = false;
                areaChilders[1].IsActive = false;
                return true;
            }
            return false;
        }
        else
        {
            if(angle >= 180)
            {
                angle -= 360;
            }
            if(angle <= deg)
            {
                isRotate = false;
                areaChilders[0].IsActive = false;
                areaChilders[1].IsActive = false;
                return true;
            }
            return false;
        }
    }

    private Vector3 SetNewAngle(float deg)
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        switch (rotateAxis)
        {
            case RotateAxis.X:
                return new Vector3(deg, rot.y, rot.z);
            case RotateAxis.Y:
                return new Vector3(rot.x, deg, rot.z);
            case RotateAxis.Z:
                return new Vector3(rot.x, rot.y, deg);
            default:
                Debug.LogError("回転軸おかしくてワロタ");
                break;
        }
        return Vector3.zero;
    }

    private Vector3 GetAxis()
    {
        switch (rotateAxis)
        {
            case RotateAxis.X:
                return Vector3.right;
            case RotateAxis.Y:
                return Vector3.up;
            case RotateAxis.Z:
                return Vector3.forward;
            default:
                Debug.LogError("回転軸おかしくてワロタ");
                break;
        }
        return Vector3.zero;
    }

    private void StopRotationSet(float deg)
    {
        switch (rotateAxis)
        {
            case RotateAxis.X:
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.x = deg;
                    Quaternion q = Quaternion.identity;
                    q.eulerAngles = rot;
                    transform.rotation = q;
                    break;
                }
                
            case RotateAxis.Y:
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.x = deg;
                    Quaternion q = Quaternion.identity;
                    q.eulerAngles = rot;
                    transform.rotation = q;
                    break;
                }
            case RotateAxis.Z:
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.x = deg;
                    Quaternion q = Quaternion.identity;
                    q.eulerAngles = rot;
                    transform.rotation = q;
                    break;
                }
            default:
                Debug.LogError("回転軸おかしくてワロタ");
                break;
        }
    }

    private void IsRotateTure()
    {
        isRotate = true;
    }
}
