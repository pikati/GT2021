using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDecide : MonoBehaviour
{
    [SerializeField]
    private GameObject[] RotatePoints;

    // Start is called before the first frame update
    void Start()
    {
        RotatePoints = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(RotatePoints[0].OnPlayer + " : " + RotatePoints[1].OnPlayer);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        SearchAxis();

        foreach(GameObject rp in RotatePoints)
        {
            RotatePoint com = rp.GetComponent<RotatePoint>();
            com.OnPlayer = true;
            
            //rp.GetComponent<RotatePoint>().OnPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        foreach (GameObject rp in RotatePoints)
        {
            rp.GetComponent<RotatePoint>().OnPlayer = false;
        }
    }

    //回転軸更新
    private void SearchAxis()
    {
        //エリアのワールド座標を取る
        Vector3 center = this.GetComponent<BoxCollider>().center;
        center = transform.TransformPoint(center);

        //+X -X +Y -Y各一番近い軸を保存
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("RotatePoint"))
        {
            float tmp_x, tmp_z, tmp_dis;
            float near_xp, near_xm, near_zp, near_zm;
            tmp_x = tmp_z = tmp_dis = near_xp = near_xm = near_zp = near_zm = 0.0f;

            tmp_x = obj.transform.position.x - center.x;
            tmp_z = obj.transform.position.z - center.z;

            //Xについてプラスマイナス
            if(tmp_x > 0.0f)
            {
                //距離を調べて一番近いやつを保存
                tmp_dis = Vector3.Distance(center, obj.transform.position);

                if (near_xp == 0 || near_xp > tmp_dis)
                {
                    near_xp = tmp_dis;
                    RotatePoints[0] = obj;
                    Debug.Log("０取った　＋X");
                }
            }
        }
    }
}

 /* ・パネルのボックスコライダーの中心を取る
 * ・↑からの距離で取る
 * 　１、ローカルで取れるので、ワールドに変換
 * 　２、箱コライダからみて
 * 　　　・X距離プラスの代表
 * 　　　・X距離マイナスの代表
 * 　　　・Z距離プラスの代表
 * 　　　・Z距離マイナスの代表
 * 　３、この処理をOTEnterで走らせる
 */
