using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-103)]
public class NavMeshTest : MonoBehaviour
{
    NavMeshSurface surface;
    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            surface.BuildNavMesh();

            yield return new WaitForSeconds(0.5f);
        }
    }
}
