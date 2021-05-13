using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor : MonoBehaviour
{
    private SlideParam slideParam;
    private InputController inputController;
    private float speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        inputController = Singleton<InputController>.Instance;
        slideParam = new SlideParam();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 move = inputController.MoveValue;
            if (move != Vector2.zero)
            {
                if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
                {
                    if (move.x > move.y)
                    {
                        move.x = speed;
                        move.y = 0;
                    }
                    else
                    {
                        move.x = -speed;
                        move.y = 0;
                    }
                }
                else
                {
                    if (move.x > move.y)
                    {
                        move.x = 0;
                        move.y = -speed;
                    }
                    else
                    {
                        move.x = 0;
                        move.y = speed;
                    }
                }
                
                slideParam.Direction = Vector3.forward * move.y + Camera.main.transform.right * move.x;
                other.GetComponent<PlayerState>().state = PlayerState.PlayerStateEnum.Slide;
                other.GetComponent<PlayerMove>().SlideParam = slideParam;
            }
            else
            {
                slideParam = other.GetComponent<PlayerMove>().SlideParam;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (slideParam.Direction.x == 0 && slideParam.Direction.z == 0) return;
            other.GetComponent<PlayerState>().state = PlayerState.PlayerStateEnum.Slide;
            other.GetComponent<PlayerMove>().SlideParam = slideParam;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerState>().state = PlayerState.PlayerStateEnum.Move;
        }
    }
}
