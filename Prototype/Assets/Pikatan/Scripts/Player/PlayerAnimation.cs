using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public float MoveValue { get; set; }
    private float animRand = 0;
    private PlayerMove playerMove;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        animRand = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Singleton<ClearChecker>.Instance.IsClear || Singleton<GameManager>.Instance.gameState != GameManager.GameState.Play)
        {
            if(animRand < 0.5)
            {
                anim.SetBool("Hand", true);
            }
            else
            {
                anim.SetTrigger("Banzai");
            }
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
