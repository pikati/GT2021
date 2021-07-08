using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisPointer : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    private InputController ic;
    private Rigidbody rb;
    private Vector3 direction;
    private MeshRenderer meshRenderer;
    private bool isGameStart = false;
    void Start()
    {
        ic = Singleton<InputController>.Instance;
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    void Update()
    {
        if (!isGameStart) return;
        if (GameManager.GameState.Play != Singleton<GameManager>.Instance.gameState) return;
        MovePointer();
    }

    private void MovePointer()
    {
        Vector2 move = ic.RightStickValue;
        if (move != Vector2.zero)
        {
            direction = Vector3.forward * move.y + Camera.main.transform.right * move.x;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * Time.deltaTime * speed;
    }


    public void ActivatePointer()
    {
        meshRenderer.enabled = true;
        isGameStart = true;
    }
}
