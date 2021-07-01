using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEffect : MonoBehaviour
{
    private GameTimer timer;
    private Vector3 oldPos = Vector3.zero;
    private GameObject walkEffect;
    // Start is called before the first frame update
    void Start()
    {
        timer = new GameTimer(0.5f);
        walkEffect = Resources.Load("WalkEffect") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(oldPos != transform.position)
        {
            if(timer.UpdateTimer())
            {
                Destroy(Instantiate(walkEffect, transform.position, Quaternion.identity), 5.0f);
                timer.ResetTimer(0.5f);
            }
        }
        oldPos = transform.position;
    }
}
