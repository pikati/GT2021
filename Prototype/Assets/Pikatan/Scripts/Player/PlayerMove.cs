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
    public SlideParam SlideParam { get; set; }//何かに当たったら速度0にする処理書くかも
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        inputController = Singleton<InputController>.Instance;
        agent = GetComponent<NavMeshAgent>();
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
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
            Vector3 move = inputController.MoveValue;
            move.z = move.y;
            move.y = 0;
            SlideParam.Direction += move * 0.01f;
            agent.Move(SlideParam.Direction * speed * Time.deltaTime);
        }
        LookDirection();
        lastPosition = transform.position;
    }

    private void LookDirection()
    {
        Vector3 diff = transform.position - lastPosition;
        if(diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }
    }
}
