using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProceduralMesh
{
    public static readonly ProceduralMesh Empty = new ProceduralMesh(Array.Empty<Vector3>(), Array.Empty<int>());

    private readonly List<Vector3> _vertices;
    private readonly List<int> _triangles;

    public ProceduralMesh(IEnumerable<Vector3> vertices, IEnumerable<int> triangles)
    {
        _vertices = new List<Vector3>(vertices);
        _triangles = new List<int>(triangles);
    }

    public Mesh ToMesh()
    {
        var mesh = new Mesh
        {
            vertices = _vertices.ToArray(),
            triangles = _triangles.ToArray(),
        };

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

    public static ProceduralMesh operator +(ProceduralMesh a, ProceduralMesh b)
    {
        return new ProceduralMesh(
            a._vertices.Concat(b._vertices),
            a._triangles.Concat(b._triangles.Select(triangle => triangle + a._vertices.Count)));
    }
}