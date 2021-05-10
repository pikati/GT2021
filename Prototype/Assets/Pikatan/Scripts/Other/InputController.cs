using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : Singleton<InputController>
{
    #region Field
    private PlayerInput input;
    private InputAction move, look, a, b, x, y, rb, rt, r3, lb, lt, l3, select, start, up, down, left, right, arrow;
    private Vector2 moveValue, rightValue, arrowValue;
    #endregion

    #region Property
    public Vector2 MoveValue => moveValue;
    public Vector2 RightStickValue => rightValue;
    public Vector2 ArrowValue => arrowValue;
    public bool A { get; private set; }
    public bool B { get; private set; }
    public bool X { get; private set; }
    public bool Y { get; private set; }
    public bool RB { get; private set; }
    public bool RT { get; private set; }
    public bool R3 { get; private set; }
    public bool LB { get; private set; }
    public bool LT { get; private set; }
    public bool L3 { get; private set; }
    public bool SelectPress { get; private set; }
    public bool StartPress { get; private set; }
    //public bool Up { get; private set; }
    //public bool Down { get; private set; }
    //public bool Left { get; private set; }
    //public bool Right { get; private set; }


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        var actionMap = input.currentActionMap;
        move = actionMap["Move"];
        look = actionMap["Look"];
        a = actionMap["A"];
        b = actionMap["B"];
        x = actionMap["X"];
        y = actionMap["Y"];
        lb = actionMap["LB"];
        lt = actionMap["LT"];
        l3 = actionMap["L3"];
        rb = actionMap["RB"];
        rt = actionMap["RT"];
        r3 = actionMap["R3"];
        select = actionMap["Select"];
        start = actionMap["Start"];
        //up = actionMap["Up"];
        //down = actionMap["Down"];
        //left = actionMap["Left"];
        //right = actionMap["Right"];
        arrow = actionMap["Arrow"];
    }

    // Update is called once per frame
    void Update()
    {
        moveValue = move.ReadValue<Vector2>();
        rightValue = look.ReadValue<Vector2>();
        arrowValue = arrow.ReadValue<Vector2>();
        A = a.triggered;
        B = b.triggered;
        X = x.triggered;
        Y = y.triggered;
        RB = rb.triggered;
        RT = rt.triggered;
        R3 = r3.triggered;
        LB = lb.triggered;
        LT = lt.triggered;
        L3 = l3.triggered;
        SelectPress = select.triggered;
        StartPress = start.triggered;
        //Up = up.triggered;
        //Down = down.triggered;
        //Left = left.triggered;
        //Right = right.triggered;
    }
}
