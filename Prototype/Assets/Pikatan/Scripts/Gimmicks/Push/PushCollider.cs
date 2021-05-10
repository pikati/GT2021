using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCollider : MonoBehaviour
{
    [SerializeField]
    private Push.ColDirection colDirection;
    private Push.ColDirection colDir = Push.ColDirection.Max;
    private Push push;
    // Start is called before the first frame update
    void Start()
    {
        colDir = colDirection;
        push = transform.parent.gameObject.GetComponent<Push>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            push.ChildDirection = colDir;
        }
    }
}
