using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    //ParticleSystem型の変数
    ParticleSystem ps;
    //GameObject型の変数
    GameObject obj;


    // Start is called before the first frame update
    void Start()
    {
        //FindメソッドでExplosionのGameObjectにアクセスして変数objで参照
        obj = GameObject.Find("Explosion");
        //GetComponentInChildrenで子要素も含めたParticleSystemコンポーネントにアクセスして変数㎰で参照
        ps = obj.GetComponentInChildren<ParticleSystem>();
        //変数objを非表示にしてParticleSystemの実行を止めておく
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックされたら爆発のParticleSystemを実行する
        if (Input.GetMouseButtonDown(0))
        {
            obj.SetActive(true);
            ps.Play();
        }

    }
}
