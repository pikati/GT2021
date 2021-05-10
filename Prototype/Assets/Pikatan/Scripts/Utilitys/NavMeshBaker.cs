using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshBaker : Singleton<NavMeshBaker>
{
    private NavMeshSurface surface;
    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
    }
    // Start is called before the first frame update
    public void Bake()
    {
        surface.BuildNavMesh();
    }
    
}
