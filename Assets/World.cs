using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Voxel _voxelPrefab;

    public Vertex[,] Vertexes { get; private set; } = new Vertex[0, 0];

    private void Start()
    {
        InitVertexes();
        CentersVertexes();
        InitVoxels();
    }

    private void InitVertexes()
    {
        Vertexes = new Vertex[128, 128];

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
                var voxel = Instantiate(_voxelPrefab, parent);
                voxel.name = $"Voxel ({column},{row})";

                voxel.Init(
                    Vertexes[column + 0, row + 0],
                    Vertexes[column + 0, row + 1],
                    Vertexes[column + 1, row + 1],
                    Vertexes[column + 1, row + 0]);
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