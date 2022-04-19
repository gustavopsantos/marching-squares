using System;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [field: SerializeField, Range(0, 1)] public float IsoValue { get; private set; } = 0.5f; 
    public Vertex[,] Vertexes { get; private set; }

    private List<Voxel> _voxels = new List<Voxel>();

    [SerializeField] private MeshFilter _meshFilter;

    private void Start()
    {
        InitVertexes();
        CentersVertexes();
        InitVoxels();
    }

    private void Update()
    {
        var procedural = ProceduralMesh.Empty;
        
        foreach (var voxel in _voxels)
        {
            procedural += GenerateVoxel.Generate(voxel, IsoValue);
        }

        _meshFilter.sharedMesh = procedural.ToMesh();
    }

    private void InitVertexes()
    {
        Vertexes = new Vertex[5, 5];

        for (int row = 0; row < Vertexes.GetLength(1); row++)
        {
            for (int column = 0; column < Vertexes.GetLength(0); column++)
            {
                Vertexes[column, row] = new Vertex(0f, new Vector2(column, row));
            }
        }
    }

    private void CentersVertexes()
    {
        foreach (var vertex in Vertexes)
        {
            vertex.Center -= new Vector2(Vertexes.GetLength(0) / 2f - 0.5f, Vertexes.GetLength(1) / 2f - 0.5f);
        }
    }

    private void InitVoxels()
    {
        var parent = new GameObject("Voxels").transform;
        parent.SetParent(transform);

        for (int row = 0; row < Vertexes.GetLength(1) - 1; row++)
        {
            for (int column = 0; column < Vertexes.GetLength(0) - 1; column++)
            {
                var voxel = new GameObject($"Voxel ({column},{row})").AddComponent<Voxel>();
                voxel.transform.SetParent(parent.transform);
                voxel.Init(
                    Vertexes[column + 0, row + 0],
                    Vertexes[column + 0, row + 1],
                    Vertexes[column + 1, row + 1],
                    Vertexes[column + 1, row + 0]);

                _voxels.Add(voxel);
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var voxel in Vertexes)
        {
            voxel.DrawGizmos();
        }
    }
}