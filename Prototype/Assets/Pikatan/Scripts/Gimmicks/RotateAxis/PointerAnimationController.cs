using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAnimationController : MonoBehaviour
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FocusPointer()
    {
        anim.SetBool("IsFocus", true);
    }

    public void ExitPointer()
    {
        anim.SetBool("IsFocus", false);
    }
}
