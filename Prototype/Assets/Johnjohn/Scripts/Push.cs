using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    //BoxCollider用変数
    private BoxCollider[] boxcol;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("開始");

        //BoxCollider取得
        boxcol = GetComponents<BoxCollider>();

        //for (int i = 0; i < col.Length; i++)
        //{
        //    col[i].enabled = false;
        //}
    }

    void Update()
    {
        


    }

    void OnTriggerStay(Collider other)//接触中判定
    {

        Debug.Log("接触中");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Translate(1.0f, 0.0f, 0.0f);
        }

    }

}
