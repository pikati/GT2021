using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NavMeshDrawer : Singleton<NavMeshDrawer>
{

    void Awake()
    {
        DrawNwvMesh();
    }

    public void DrawNwvMesh()
    {
        //NavMeshの三角形集合取得
        NavMeshTriangulation triangles = NavMesh.CalculateTriangulation();

        //三角形集合からMeshを生成
        Mesh mesh = new Mesh();
        mesh.vertices = triangles.vertices;
        mesh.triangles = triangles.indices;

        //MeshFilterに生成したMeshを渡す
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }
}