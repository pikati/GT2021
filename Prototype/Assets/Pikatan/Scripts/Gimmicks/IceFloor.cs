using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor : MonoBehaviour
{
    SlideParam slideParam;
    InputController inputController;
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
