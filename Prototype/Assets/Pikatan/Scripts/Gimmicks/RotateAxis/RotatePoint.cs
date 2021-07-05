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
    public bool IsActive { get; set; }

    [SerializeField]
    private float rotateSpeed;
    private RotateAxis rotateAxis = RotateAxis.Z;
    private RotateState rotateState = RotateState.NoRotate;
    private bool isRotate = false;
    private float rotateValue;
    private float[] angles = new float[3];
    private ChangeColor changeColor;
    private AreaChilder[] areaChilders = new AreaChilder[2];
    private PointerSuitsuki obj;
    private GameObject effect;


    public bool OnPlayer { get; set; } = false;

    private void Start()
    {
        SetRotateValue();
        changeColor =GetComponentInChildren<ChangeColor>();
        areaChilders[0] = transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<AreaChilder>();
        areaChilders[1] = transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<AreaChilder>();
        obj = GameObject.Find("SuitsukiObj").GetComponent<PointerSuitsuki>();
        effect = Resources.Load("SelectEffect") as GameObject;
    }


    // Update is called once per frame
    void Update()
    {
        if(OnPlayer)
        {
            changeColor.AState = ChangeColor.AxisState.OnPlayer;
        }
        
        if (isRotate)
        {
            float deg = rotateState == RotateState.NoRotate ? 180.0f : 0;
            float angleSpeed = rotateValue * rotateSpeed * Time.deltaTime;
            angles[(int)rotateAxis] += angleSpeed;
            transform.rotation = transform.rotation * Quaternion.AngleAxis(angleSpeed, GetAxis());
            Singleton<RotatePointSelector>.Instance.IsRotating = true;
            Singleton<PlayerMove>.Instance.SaveDirection();

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
                Bake();
                Singleton<AxisStateController>.Instance.AxisState = AxisStateController.AxisStateEnum.NoRotate;
                Singleton<RotatePointSelector>.Instance.IsRotating = false;
                Singleton<PlayerMove>.Instance.LoadDirection();
                Singleton<SoundManager>.Instance.PlaySeByName("endRotate");
                GameObject efc = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(efc, 1.0f);
            }
        }
    }

    public void BeginRotate()
    {
        if (!IsActive) return;
        if (OnPlayer) return;
        if (Singleton<AxisStateController>.Instance.AxisState == AxisStateController.AxisStateEnum.Rotating) return;
        if (Singleton<GameManager>.Instance.gameState != GameManager.GameState.Play) return;
        if (!Singleton<StageStart>.Instance.IsEnd) return;

        Singleton<SoundManager>.Instance.PlaySeByName("startRotate");
        Invoke("IsRotateTure", 0.05f);
        SetRotateValue();
        areaChilders[0].IsActive = true;
        areaChilders[1].IsActive = true;
        //Singleton<CameraRotater>.Instance.CameraRotate();
       // Singleton<NavMeshDrawer>.Instance.DrawNwvMesh();
    }

    //初期化と回転フラグ立った時に呼べ
    private void SetRotateValue()
    {
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

    //参照無いのはBeginRotateでInvokeしてるからだよ
    private void IsRotateTure()
    {
        isRotate = true;
        Singleton<AxisStateController>.Instance.AxisState = AxisStateController.AxisStateEnum.Rotating;
        Invoke("Bake", 0.1f);
    }

    private void Bake()
    {
        Singleton<NavMeshBaker>.Instance.Bake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AxisPointer"))
        {
            Singleton<RotatePointSelector>.Instance.SetSelectAxis(gameObject);
            obj.SetSelectObject(this);
            IsActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AxisPointer"))
        {
            Singleton<RotatePointSelector>.Instance.SetSelectAxis(null);
            IsActive = false;
        }
    }
}
