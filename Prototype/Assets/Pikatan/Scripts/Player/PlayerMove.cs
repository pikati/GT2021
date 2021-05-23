using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : Singleton<PlayerMove>
{
    [SerializeField]
    private float speed = 1.0f;

    private InputController inputController;
    private NavMeshAgent agent;
    private PlayerState playerState;
    private Vector3 lastPosition;
    private float iceSpeed = 4.0f;
    private Vector3 saveDirection;
    private PlayerAnimation playerAnimation;
    public SlideParam SlideParam { get; set; } = null;//何かに当たったら速度0にする処理書くかも
    private Rigidbody rb;

    void Start()
    {
        inputController = Singleton<InputController>.Instance;
        agent = GetComponent<NavMeshAgent>();
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        SlideParam = new SlideParam();
        playerAnimation = transform.Find("MEBIZO").GetComponent<PlayerAnimation>();
        agent.enabled = false;
    }

    void Update()
    {
        if (GameManager.GameState.Play != Singleton<GameManager>.Instance.gameState) return;
        CheckPanel();
        Move();
        if(inputController.R3)
        {
            Singleton<ClearChecker>.Instance.DebugClear();
        }
    }

    private void CheckPanel()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), -Vector3.up);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red);
        if (Physics.Raycast(ray, out hit, 0.1f))
        {
            if (hit.collider.CompareTag("Panel"))
            {
                ChangePlayerState(PlayerState.PlayerStateEnum.Move);
            }
            else if (hit.collider.CompareTag("IcePanel"))
            {
                ChangePlayerState(PlayerState.PlayerStateEnum.Slide);
            }
        }
    }

    private void ChangePlayerState(PlayerState.PlayerStateEnum state)
    {
        if (state == playerState.state) return;
        playerState.state = state;
        switch (state)
        {
            case PlayerState.PlayerStateEnum.Move:
                SlideParam.Direction = Vector3.zero;
                break;
            case PlayerState.PlayerStateEnum.Slide:
                SetSlideDirection();
                break;
        }

    }

    private void Move()
    {
        if (playerState.state == PlayerState.PlayerStateEnum.Move)
        {
            Vector2 move = inputController.MoveValue;
            playerAnimation.MoveValue = Mathf.Abs(move.x) + Mathf.Abs(move.y);
            //var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            if (move != Vector2.zero)
            {
                Vector3 direction = Vector3.forward * move.y + Camera.main.transform.right * move.x;
                agent.Move(direction * speed * Time.deltaTime);
                
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else if (playerState.state == PlayerState.PlayerStateEnum.Slide)
        {
            SetSlideDirection();

        }
        LookDirection();
        lastPosition = transform.position;
    }

    private void SetSlideDirection()
    {
        if (Singleton<RotatePointSelector>.Instance.IsRotating) return;
        agent.Move(SlideParam.Direction * speed * Time.deltaTime);

        if (Vec3Abs(transform.position, lastPosition) < 0.001f)
        {
            playerState.state = PlayerState.PlayerStateEnum.Move;
            Vector3 move = inputController.MoveValue;
            move.z = move.y;
            move.y = 0;
            if (move.x == 0 && move.y == 0)
            {
                SlideParam.Direction = Vector2.zero;
                lastPosition = transform.position;
                return;
            }
            else
            {
                if (Mathf.Abs(move.x) > Mathf.Abs(move.z))
                {
                    if (move.x > move.z)
                    {
                        move.x = iceSpeed;
                        move.z = 0;
                    }
                    else
                    {
                        move.x = -iceSpeed;
                        move.z = 0;
                    }
                }
                else
                {
                    if (move.x > move.z)
                    {
                        move.x = 0;
                        move.z = -iceSpeed;
                    }
                    else
                    {
                        move.x = 0;
                        move.z = iceSpeed;
                    }
                }
                SlideParam.Direction = move;
            }



            lastPosition = transform.position;
        }
    }

    private void LookDirection()
    {
        Vector3 moveDir = inputController.MoveValue;
        moveDir.z = moveDir.y;
        moveDir.y = 0;
        if (moveDir.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }

    private float Vec3Abs(Vector3 a, Vector3 b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 3.0f;
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("IceStopper"))
    //    {
    //        playerState.state = PlayerState.PlayerStateEnum.Move;
    //        SlideParam.Direction = Vector3.zero;
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    //if (other.CompareTag("Obstacle"))
    //    //{
    //    //    if (isMove) return;
    //    //    playerState.state = PlayerState.PlayerStateEnum.Move;
    //    //    SlideParam.Direction = Vector3.zero;
    //    //}
    //    //if (other.CompareTag("IcePanel"))
    //    //{
    //    //    if (!isMove) return;
    //    //    other.GetComponent<IceFloor>().SetSlideDirection(gameObject);
    //    //}
    //    if (other.CompareTag("Panel"))
    //    {
    //        if (playerState.state == PlayerState.PlayerStateEnum.Slide)
    //        {
    //            playerState.state = PlayerState.PlayerStateEnum.Move;
    //        }
    //    }

    //}

    public void SaveDirection()
    {
        saveDirection = SlideParam.Direction;
    }

    public void LoadDirection()
    {
        SlideParam.Direction = saveDirection;
        saveDirection = Vector3.zero;
        lastPosition = new Vector3(100, 100, 100);
    }

    public void ActivateAgent()
    {
        agent.enabled = true;
    }
}
