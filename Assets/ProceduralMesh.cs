using UnityEngine;
using System.Collections.Generic;

public class ProceduralMesh
{
    public readonly Mesh Mesh = new Mesh();
    public readonly List<Vector3> Vertices = new List<Vector3>();
    public readonly List<int> Triangles = new List<int>();

    public void Build()
    {
        Mesh.Clear();
        Mesh.SetVertices(Vertices);
        Mesh.SetTriangles(Triangles, 0);
        Mesh.RecalculateBounds();
        Mesh.RecalculateNormals();
    }
}