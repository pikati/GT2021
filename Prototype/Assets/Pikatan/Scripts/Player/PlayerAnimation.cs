using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public float MoveValue { get; set; }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Singleton<ClearChecker>.Instance.IsClear)
        {
            anim.SetFloat("Move", 0);
        }
        else
        {
            anim.SetFloat("Move", MoveValue);
        }
    }
}
