using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private InputController ic;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("RotatePoint");
        ic = Singleton<InputController>.Instance;
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
            
            if (ic.A)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().BeginRotate();
            }
        }
    }

    public void SetSelectableObjects()
    {
        for(int i = 0; i < 4; i++)
        {
            selectableObjects[i] = null;
        }
        RotatePoint rotatePoint = selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>();
        selectableObjects[0] = rotatePoint.selectableObjs[0];
        selectableObjects[1] = rotatePoint.selectableObjs[1];
        selectableObjects[2] = rotatePoint.selectableObjs[2];
        selectableObjects[3] = rotatePoint.selectableObjs[3];
        
        //SetForntObject();
        //SetBackObject();
        //SetLeftObject();
        //SetRightObject();
    }

    private void SetForntObject()
    {
        Vector3 selectPoint = new Vector3(0, 0, 10000);
        foreach(GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.z - selectableObjects[(int)SelectDirection.Own].transform.position.z;
            if (length > 0)
            {
                if(length < selectPoint.z - selectableObjects[(int)SelectDirection.Own].transform.position.z)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[0] = pointObj;
                }
            }
        }
    }

    private void SetBackObject()
    {
        Vector3 selectPoint = new Vector3(0, 0, -10000);
        foreach (GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.z - selectableObjects[(int)SelectDirection.Own].transform.position.z;
            if (length < 0)
            {
                if (length > selectPoint.x - selectableObjects[(int)SelectDirection.Own].transform.position.z)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[1] = pointObj;
                }
            }
        }
    }
    private void SetLeftObject()
    {
        Vector3 selectPoint = new Vector3(-10000, 0, 0);
        foreach (GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.x - selectableObjects[(int)SelectDirection.Own].transform.position.x;
            if (length < 0)
            {
                if (length > selectPoint.x - selectableObjects[(int)SelectDirection.Own].transform.position.x)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[2] = pointObj;
                }
            }
        }
    }
    private void SetRightObject()
    {
        Vector3 selectPoint = new Vector3(10000, 0, 0);
        foreach (GameObject pointObj in rotateObjects)
        {
            float length = pointObj.transform.position.x - selectableObjects[(int)SelectDirection.Own].transform.position.x;
            if (length > 0)
            {
                if (length < selectPoint.x - selectableObjects[(int)SelectDirection.Own].transform.position.x)
                {
                    selectPoint = pointObj.transform.position;
                    selectableObjects[3] = pointObj;
                }
            }
        }
    }

    private void SelectObject()
    {
        if (ic.Up)
        {
            if (selectableObjects[(int)SelectDirection.Front] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Front].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Front];
            }
        }
        if (ic.Down)
        {
            if (selectableObjects[(int)SelectDirection.Back] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Back].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Back];
            }
        }
        if (ic.Left)
        {
            if (selectableObjects[(int)SelectDirection.Left] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Left].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Left];
            }
        }
        if (ic.Right)
        {
            if (selectableObjects[(int)SelectDirection.Right] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Right].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Right];
            }
        }
        SetSelectableObjects();
    }
}
