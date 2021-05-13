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
    void Start()
    {
        ic = Singleton<InputController>.Instance;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
