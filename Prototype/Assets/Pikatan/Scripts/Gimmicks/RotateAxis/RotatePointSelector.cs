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

    private GameObject selectObject = null;
    private List<GameObject> rotateObjects = new List<GameObject>();
    private GameObject[] selectableObjects = new GameObject[5];
    private InputController ic;
    private bool isInput = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("RotatePoint");
        ic = Singleton<InputController>.Instance;
        foreach(GameObject obj in objs)
        {
            rotateObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Singleton<GameManager>.Instance.gameState != GameManager.GameState.Play) return;
        if (Singleton<StageState>.Instance.NowStageState == StageState.StageStateEnum.Rotate)
        {
            //SelectObject();
            
            if (ic.RB)
            {
                if (selectObject == null) return;
                RotatePoint r = selectObject.GetComponent<RotatePoint>();
                if(r != null)
                {
                    selectObject.GetComponent<RotatePoint>().BeginRotate();
                }
            }
        }
    }

    private void SetSelectableObjects()
    {
        //for(int i = 0; i < 4; i++)
        //{
        //    selectableObjects[i] = null;
        //}
        //RotatePoint rotatePoint = selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>();
        //selectableObjects[0] = rotatePoint.selectableObjs[0];
        //selectableObjects[1] = rotatePoint.selectableObjs[1];
        //selectableObjects[2] = rotatePoint.selectableObjs[2];
        //selectableObjects[3] = rotatePoint.selectableObjs[3];
    }

    private void SelectObject()
    {
        if(ic.ArrowValue == Vector2.zero)
        {
            isInput = false;
        }
        if (isInput) return;
        if (ic.ArrowValue.y > 0)
        {
            isInput = true;
            if (selectableObjects[(int)SelectDirection.Front] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Front].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Front];
            }
        }
        if (ic.ArrowValue.y < 0)
        {
            isInput = true;
            if (selectableObjects[(int)SelectDirection.Back] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Back].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Back];
            }
        }
        if (ic.ArrowValue.x < 0)
        {
            isInput = true;
            if (selectableObjects[(int)SelectDirection.Left] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Left].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Left];
            }
        }
        if (ic.ArrowValue.x > 0)
        {
            isInput = true;
            if (selectableObjects[(int)SelectDirection.Right] != null)
            {
                selectableObjects[(int)SelectDirection.Own].GetComponent<RotatePoint>().IsActive = false;
                selectableObjects[(int)SelectDirection.Right].GetComponent<RotatePoint>().IsActive = true;
                selectableObjects[(int)SelectDirection.Own] = selectableObjects[(int)SelectDirection.Right];
            }
        }
        SetSelectableObjects();
    }

    public void SetSelectAxis(GameObject obj)
    {
        selectObject = obj;
    }
}
