using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : Singleton<InputController>
{
    #region Field
    private PlayerInput input;
    private InputAction move, look;
    private Vector2 moveValue, rightValue;
    #endregion

    #region Property
    public Vector2 MoveValue => moveValue;
    public Vector2 RightStickValue => rightValue;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        var actionMap = input.currentActionMap;
        move = actionMap["Move"];
        look = actionMap["Look"];
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = move.ReadValue<Vector2>();
        rightValue = look.ReadValue<Vector2>();
    }
}
