using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGObjRotator : MonoBehaviour
{
    float xSpeed;
    float ySpeed;
    float zSpeed;
    // Start is called before the first frame update
    void Start()
    {
        xSpeed = Random.Range(0, 30);
        ySpeed = Random.Range(0, 30);
        zSpeed = Random.Range(0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime);
    }
}
