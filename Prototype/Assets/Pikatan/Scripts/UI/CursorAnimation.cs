using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAnimation: MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAniamtion()
    {
        animator.SetTrigger("Rotate");
        Invoke("EndAnimation", 0.1f);
    }

    private void EndAnimation()
    {
        animator.SetTrigger("End");
    }
}
