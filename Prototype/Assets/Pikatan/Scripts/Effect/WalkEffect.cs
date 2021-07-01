using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEffect : MonoBehaviour
{
    private GameTimer timer;
    private Vector3 oldPos = Vector3.zero;
    private GameObject walkEffect;
    private readonly float emmitTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        timer = new GameTimer(emmitTime);
        walkEffect = Resources.Load("WalkMeshEffect") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(oldPos.x != transform.position.x || oldPos.z != transform.position.z)
        {
            if(timer.UpdateTimer())
            {
                Destroy(Instantiate(walkEffect, transform.position, Quaternion.identity), 0.5f);
                timer.ResetTimer(emmitTime);
            }
        }
        oldPos = transform.position;
    }
}
