using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//選択できるもの探索する処理で位置によって複数の方向に同じオブジェクトが設定されるバグがある（予想）
public class RotatePointSelector : Singleton<RotatePointSelector>
{
    private enum SelectDirection
    { 
        Front,
        Back,
        Left,
        Right,
        Own
    };

    [SerializeField]
    private GameObject defaultSelectObject;
    private List<GameObject> rotateObjects = new List<GameObject>();
    private GameObject[] selectableObjects = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("RotatePoint");
        foreach(GameObject obj in objs)
        {
            rotateObjects.Add(obj);
        }
        selectableObjects[(int)SelectDirection.Own] = defaultSelectObject;
    }

    // Update is called once per frame
    void Update()
    {
        //ここに動く状態か選択状態かを取得する処理 このobjectが選択状態化も必要
        if(Singleton<StageState>.Instance.NowStageState == StageState.StageStateEnum.Rotate)
        {
            SelectObject();
            
            if (Keyboard.current.qKey.isPressed)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().BeginRotate();
            }

            if(selectableObjects[(int)SelectDirection.Front] != null)
            {
                Debug.Log("Front" + selectableObjects[(int)SelectDirection.Front].name);

            }
            if (selectableObjects[(int)SelectDirection.Front] != null)
            {
                Debug.Log("back " + selectableObjects[(int)SelectDirection.Back].name);
            }
            if (selectableObjects[(int)SelectDirection.Front] != null)
            {
                Debug.Log("Left" + selectableObjects[(int)SelectDirection.Left].name);

            }
            if (selectableObjects[(int)SelectDirection.Front] != null)
            {
                Debug.Log("Right" + selectableObjects[(int)SelectDirection.Right].name);
            }
            if (selectableObjects[(int)SelectDirection.Front] != null)
            {
                Debug.Log("Own  " + selectableObjects[(int)SelectDirection.Own].name);
            }
        }
    }

    public void SetSelectableObjects()
    {

        for(int i = 0; i < 4; i++)
        {
            selectableObjects[i] = null;
        }
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        SetForntObject(playerPosition);
        SetBackObject(playerPosition);
        SetLeftObject(playerPosition);
        SetRightObject(playerPosition);
    }

    private void SetForntObject(Vector3 playerPosition)
    {
        Vector3 selectPoint = new Vector3(0, 0, 10000);
        foreach(GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.z - playerPosition.z;
            if (length > 0)
            {
                if(length < selectPoint.x - playerPosition.z)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[0] = pointObj;
                }
            }
        }
    }

    private void SetBackObject(Vector3 playerPosition)
    {
        Vector3 selectPoint = new Vector3(0, 0, -10000);
        foreach (GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.z - playerPosition.z;
            if (length < 0)
            {
                if (length > selectPoint.x - playerPosition.z)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[1] = pointObj;
                }
            }
        }
    }
    private void SetLeftObject(Vector3 playerPosition)
    {
        Vector3 selectPoint = new Vector3(-10000, 0, 0);
        foreach (GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.x - playerPosition.x;
            if (length < 0)
            {
                if (length > selectPoint.x - playerPosition.x)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[2] = pointObj;
                }
            }
        }
    }
    private void SetRightObject(Vector3 playerPosition)
    {
        Vector3 selectPoint = new Vector3(10000, 0, 0);
        foreach (GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.x - playerPosition.x;
            if (length > 0)
            {
                if (length < selectPoint.x - playerPosition.x)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[3] = pointObj;
                }
            }
        }
    }

    private void SelectObject()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            if (selectableObjects[(int)SelectDirection.Front] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Front].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Front];
            }
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            if (selectableObjects[(int)SelectDirection.Back] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Back].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Back];
            }
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            if (selectableObjects[(int)SelectDirection.Left] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Left].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Left];
            }
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            if (selectableObjects[(int)SelectDirection.Right] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Right].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Right];
            }
        }
    }
}
