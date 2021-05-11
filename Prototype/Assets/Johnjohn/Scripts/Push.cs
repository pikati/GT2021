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
    [SerializeField]
    private ColDirection colliderDirection;
    private GameTimer bakeTimer = new GameTimer(0.25f);
    public ColDirection ColliderDirection { get; private set; }
    public ColDirection ChildDirection { get; set; } = ColDirection.Max;

    public enum ColDirection
    {
        Front,
        Back,
        Left,
        Right,
        Max
    }

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
        ColliderDirection = colliderDirection;
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
            if (ColliderDirection != ChildDirection) return;
            if(pushMoveState==PushMoveState.Stop)
            {
                pushMoveState = PushMoveState.Move;
            }
        }
    }

    private void Move()
    {
        Vector3 adjust = new Vector3(0, 0.25f, 0);
        if (pushMoveState == PushMoveState.Stop)
            return;

        if(pushState==PushState.Uninit)
        {

            transform.position = Vector3.MoveTowards(transform.position, startObj.transform.position + adjust , speed * Time.deltaTime);
            if (IsMoveCompleted(startObj.transform.position + adjust))
            {
                ChengeState();
                //Singleton<NavMeshBaker>.Instance.Bake();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endObj.transform.position + adjust, speed * Time.deltaTime);
            if (IsMoveCompleted(endObj.transform.position + adjust))
            {
                ChengeState();
                //Singleton<NavMeshBaker>.Instance.Bake();
            }
        }
        //if(bakeTimer.UpdateTimer())
        //{
        //    Singleton<NavMeshBaker>.Instance.Bake();
        //    bakeTimer.ResetTimer(0.25f);
        //}
    }

    private bool IsMoveCompleted(Vector3 Target)
    {
        if (Vec3Abs(transform.position, Target) <= 0.001f)
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
        switch (ColliderDirection)
        {
            case ColDirection.Front:
                ColliderDirection = ColDirection.Back;
                break;
            case ColDirection.Back:
                ColliderDirection = ColDirection.Front;
                break;
            case ColDirection.Left:
                ColliderDirection = ColDirection.Right;
                break;
            case ColDirection.Right:
                ColliderDirection = ColDirection.Left;
                break;
            default:
                Debug.LogError("ColDirectionがMax");
                break;
        }

    }

    private void Bake()
    {
        Singleton<NavMeshBaker>.Instance.Bake();
    }
}
