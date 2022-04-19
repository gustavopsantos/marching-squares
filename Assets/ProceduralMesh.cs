using UnityEngine;
using System.Collections.Generic;

public class ProceduralMesh
{
    public static readonly ProceduralMesh Empty = new ProceduralMesh(new List<Vector3>(), new List<int>());
    
    private readonly List<Vector3> _vertices;
    private readonly List<int> _triangles;

    public ProceduralMesh(IReadOnlyList<Vector3> vertices, IReadOnlyList<int> triangles)
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
        var result = new ProceduralMesh(a._vertices, a._triangles);
        
        // Merging vertices
        result._vertices.AddRange(b._vertices);
        
        // Remapping and merging triangles
        for (int i = 0; i < b._triangles.Count; i++)
        {
            var remappedTriangle = b._triangles[i] + a._vertices.Count;
            result._triangles.Add(remappedTriangle);
        }

        return result;
    }
}