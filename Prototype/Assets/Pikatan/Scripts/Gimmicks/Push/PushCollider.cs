using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCollider : MonoBehaviour
{
    private Push.ColDirection colDir = Push.ColDirection.Max;
    private Push push;
    // Start is called before the first frame update
    void Start()
    {
        push = transform.parent.gameObject.GetComponent<Push>();
        SetDirection();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            push.ChildDirection = colDir;
        }
    }

    private void SetDirection()
    {
        if(gameObject.name == "Front")
        {
            colDir = Push.ColDirection.Front;
        }
        if (gameObject.name == "Back")
        {
            colDir = Push.ColDirection.Back;
        }
        if (gameObject.name == "Left")
        {
            colDir = Push.ColDirection.Left;
        }
        if (gameObject.name == "Right")
        {
            colDir = Push.ColDirection.Right;
        }
    }
}
