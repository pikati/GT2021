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
    private InputController ic;
    public bool IsRotating { get; set; }

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("RotatePoint");
        ic = Singleton<InputController>.Instance;
        foreach(GameObject obj in objs)
        {
            rotateObjects.Add(obj);
        }
    }


    void Update()
    {
        if (Singleton<GameManager>.Instance.gameState != GameManager.GameState.Play) return;
        if (!Singleton<StageStart>.Instance.IsEnd) return;
        if (Singleton<ClearChecker>.Instance.IsClear) return;
        if (Singleton<StageState>.Instance.NowStageState == StageState.StageStateEnum.Rotate)
        {
            if (ic.A)
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

    public void SetSelectAxis(GameObject obj)
    {
        selectObject = obj;
    }

    public GameObject GetSelectObject()
    {
        return selectObject;
    }
}
