using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool isReach = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isReach) return;
        if(other.CompareTag("Player"))
        {
            isReach = true;
            Singleton<ClearChecker>.Instance.ReachChechkPoint();
            Destroy(gameObject);
            
        }
    }

    private void OnDestroy()
    {
        Singleton<NavMeshBaker>.Instance.Bake();
    }
}
