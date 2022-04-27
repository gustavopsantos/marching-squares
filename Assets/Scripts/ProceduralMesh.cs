using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProceduralMesh
{
    public static readonly ProceduralMesh Empty = new ProceduralMesh(Array.Empty<Vector3>(), Array.Empty<int>());

    public readonly Mesh Mesh;

    public ProceduralMesh(IEnumerable<Vector3> vertices, IEnumerable<int> triangles)
    {
        Mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
        };
        
        Mesh.RecalculateBounds();
        Mesh.RecalculateNormals();
    }
}