using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private InputController inputController;
    private NavMeshAgent agent;
    private PlayerState playerState;
    private Vector3 lastPosition;
    private float iceSpeed = 4.0f;
    private bool isMove = true;
    public SlideParam SlideParam { get; set; } = null;//何かに当たったら速度0にする処理書くかも
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        inputController = Singleton<InputController>.Instance;
        agent = GetComponent<NavMeshAgent>();
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        SlideParam = new SlideParam();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameState.Play != Singleton<GameManager>.Instance.gameState) return;
        Move();
    }

    private void Move()
    {
        if(playerState.state == PlayerState.PlayerStateEnum.Move)
        {
            Vector2 move = inputController.MoveValue;
            //var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            if (move != Vector2.zero)
            {
                isMove = true;
                Vector3 direction = Vector3.forward * move.y + Camera.main.transform.right * move.x;
                agent.Move(direction * speed * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else if(playerState.state == PlayerState.PlayerStateEnum.Slide)
        {
            Debug.Log(SlideParam.Direction);
            agent.Move(SlideParam.Direction * speed * Time.deltaTime);
            if (Vec3Abs(transform.position, lastPosition) < 0.001f)
            {
                isMove = false;
            }
            //Vector3 move = inputController.MoveValue;
            //move.z = move.y;
            //move.y = 0;
            //if (Vec3Abs(transform.position, lastPosition) < 0.001f)
            //{
            //    if (move.x == 0 && move.y == 0)
            //    {
            //        SlideParam.Direction = Vector2.zero;
            //        lastPosition = transform.position;
            //        return;
            //    }
            //    else
            //    {
            //        if (Mathf.Abs(move.x) > Mathf.Abs(move.z))
            //        {
            //            if (move.x > move.z)
            //            {
            //                move.x = iceSpeed;
            //                move.z = 0;
            //            }
            //            else
            //            {
            //                move.x = -iceSpeed;
            //                move.z = 0;
            //            }
            //        }
            //        else
            //        {
            //            if (move.x > move.z)
            //            {
            //                move.x = 0;
            //                move.z = -iceSpeed;
            //            }
            //            else
            //            {
            //                move.x = 0;
            //                move.z = iceSpeed;
            //            }
            //        }
            //        SlideParam.Direction = move;
            //    }
            //}


                //lastPosition = transform.position;
        }
        LookDirection();
        lastPosition = transform.position;
    }

    private void LookDirection()
    {
        Vector3 moveDir = inputController.MoveValue;
        moveDir.z = moveDir.y;
        moveDir.y = 0;
        if(moveDir.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }

    private float Vec3Abs(Vector3 a, Vector3 b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 3.0f;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Obstacle") || other.CompareTag("Panel"))
        {
            playerState.state = PlayerState.PlayerStateEnum.Move;
            SlideParam.Direction = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (isMove) return;
            playerState.state = PlayerState.PlayerStateEnum.Move;
            SlideParam.Direction = Vector3.zero;
        }
        if(other.CompareTag("IcePanel"))
        {
            if (!isMove) return;
            other.GetComponent<IceFloor>().SetSlideDirection(gameObject);
        }
    }

    public void SetPlayerState(PlayerState.PlayerStateEnum state)
    {
        playerState.state = state;
    }
}
