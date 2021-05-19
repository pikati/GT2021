using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    [SerializeField]
    private float startTime;
    [SerializeField]
    private float loopTime;
    private GameTimer animTimer;
    private Animator animator;
    private bool isAnimStart = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animTimer = new GameTimer(startTime);
    }

    private void Update()
    {
        if(animTimer.UpdateTimer())
        {
            animator.SetTrigger("Start");
            animTimer.ResetTimer(loopTime);
        }
        else
        {
            animator.SetTrigger("Idol");
        }
    }
}
