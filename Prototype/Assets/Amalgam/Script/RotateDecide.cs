using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotateDecide : MonoBehaviour
{
    //直接的には関係ないけど問題点
    //プレイヤーがエリアの境目にある壁にグリグリしている時
    //隣のエリアに入っている判定になってしまう(つまり隣のエリアが回転できなくなる)
    
    //将来的に発生するかもしれない問題点
    //正方形じゃないエリアが来たら
    //１、検索範囲がおかしくなる
    //２、そもそもボックスコライダーじゃなくなったら大変なことになる
    [SerializeField]
    private List<RotatePoint> RotatePointList;
    [SerializeField]
    private float Range;

    // Start is called before the first frame update
    void Start()
    {
        //回転軸の検索範囲設定
        Range = (Mathf.Max(GetComponent<BoxCollider>().size.x, GetComponent<BoxCollider>().size.z) + 0.2f) / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        SearchAxis();
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        foreach (RotatePoint rp in RotatePointList)
        {
            if (rp != null)
            {
                rp.OnPlayer = true;
            }
            else
            {
                Debug.Log("RotatePoint：nullです");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        foreach (RotatePoint rp in RotatePointList)
        {
            if (rp != null)
            {
                rp.OnPlayer = false;
            }
            else
            {
                Debug.Log("RotatePoint：nullです");
            }
        }
    }

    //回転軸更新
    private void SearchAxis()
    {
        //エリアのワールド座標を取る
        Vector3 center = this.GetComponent<BoxCollider>().center;
        center = transform.TransformPoint(center);

        //リセット
        RotatePointList.Clear();

        //範囲内の軸を全部取得する
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("RotatePoint"))
        {
            float tmp_dis = Vector3.Distance(center, obj.transform.position);

            if(tmp_dis < Range)
            {
                if (!RotatePointList.Contains(obj.GetComponent<RotatePoint>()))
                {
                    RotatePointList.Add(obj.GetComponent<RotatePoint>());
                }
            }
        }
    }
}
