using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotate_A : MonoBehaviour
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
    private float rotateSpeed;
    [SerializeField]
    private RotateAxis rotateAxis = RotateAxis.Z;
    private RotateState rotateState = RotateState.NoRotate;
    private bool isRotate = false;
    //private Vector3 rotateValue = Vector3.zero;
    private float rotateValue;
    private float[] angles = new float[3];

    private void Start()
    {
        SetRotateValue();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            isRotate = true;
            SetRotateValue();
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
                isRotate = false;
                if (rotateState == RotateState.NoRotate)
                {
                    rotateState = RotateState.Rotated;
                }
                else
                {
                    rotateState = RotateState.NoRotate;
                }
                Singleton<NavMeshBaker>.Instance.Bake();
            }
        }
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
            if (angle >= deg)
            {
                return true;
            }
            return false;
        }
        else
        {
            if (angle >= 180)
            {
                angle -= 360;
            }
            if (angle <= deg)
            {
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
}
