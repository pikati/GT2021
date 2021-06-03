using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public float MoveValue { get; set; }
    private PlayerMove playerMove;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Singleton<ClearChecker>.Instance.IsClear || Singleton<GameManager>.Instance.gameState != GameManager.GameState.Play)
        {
            anim.SetFloat("Move", 0);
        }
        else
        {
            anim.SetFloat("Move", MoveValue);
        }
    }

    public void Knee()
    {
        if (MoveValue != 0) return; 
        anim.SetTrigger("Knee");
    }
}
