using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField]
    private GameObject startObj;
    [SerializeField]
    private GameObject endObj;
    [SerializeField]
    private float speed = 1.0f;

    private enum PushState
    {
        Init,
        Uninit
    };

    private enum PushMoveState
    {
        Stop,
        Move
    };

    private PushState pushState = PushState.Init;
    private PushMoveState pushMoveState = PushMoveState.Stop;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("s" + startObj.transform.position);
        Debug.Log("e" + endObj.transform.position);

    }

    // Update is called once per frame
    void Update()
    {

        Move();
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(pushMoveState==PushMoveState.Stop)
            {
                pushMoveState = PushMoveState.Move;
            }
        }
    }

    private void Move()
    {
        if (pushMoveState == PushMoveState.Stop)
            return;

        if(pushState==PushState.Uninit)
        {
            transform.position = Vector3.Lerp(transform.position, startObj.transform.position, speed * Time.deltaTime);
            if (IsMoveCompleted(startObj.transform.position))
                ChengeState();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, endObj.transform.position, speed * Time.deltaTime);
            if (IsMoveCompleted(endObj.transform.position))
                ChengeState();
        }

        
    }

    private bool IsMoveCompleted(Vector3 Target)
    {
        if (Vec3Abs(transform.position, Target) <= 0.1f)
            return true;
        return false;

    }

    private float Vec3Abs(Vector3 a, Vector3 b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 3.0f;
    }

    private void ChengeState()
    {
        if (PushState.Init==pushState)
        {
            pushState = PushState.Uninit;
        }
        else
        {
            pushState = PushState.Init;
        }

        if (PushMoveState.Stop == pushMoveState)
        {
            pushMoveState = PushMoveState.Move;
        }
        else
        {
            pushMoveState = PushMoveState.Stop;
        }

    }
}
