using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 target = new Vector3(0, 0.16f, 1);
    private TargetSelector targetSelector;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetSelector = GetComponent<TargetSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = target;
        if (MoveComplete())
        {
            //target = new Vector3(0, 0.16f, 0);
            target = targetSelector.SelectTargetPosition();
        }
    }

    private bool MoveComplete()
    {
        if (Mathf.Abs(target.x - transform.position.x) < 0.01f && Mathf.Abs(target.y - transform.position.y) < 0.01f && Mathf.Abs(target.z - transform.position.z) < 0.01f)
        {
            return true;
        }
        return false;
    }
}
