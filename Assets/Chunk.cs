using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour
{
    [SerializeField] private Voxel _voxelPrefab;
    
    private int _x;
    private int _y;
    private int _width;
    private int _height;
    public Vertex[,] Vertexes { get; private set; } = new Vertex[0, 0];

    public void Init(int x, int y, int widthInVoxels, int heightInVoxels)
    {
        _x = x;
        _y = y;
        _width = widthInVoxels;
        _height = heightInVoxels;
        name = $"Chunk ({x},{y})";
        //transform.position = new Vector3(x, y);
        InitVertexes();
        InitVoxels();
    }
    
    private void InitVertexes()
    {
        Vertexes = new Vertex[_width+1, _height+1];

        for (int row = 0; row < Vertexes.GetLength(1); row++)
        {
            for (int column = 0; column < Vertexes.GetLength(0); column++)
            {
                Vertexes[column, row] = new Vertex(0f, new Vector2(column + _x, row + _y));
            }
        }
    }
    
    private void InitVoxels()
    {
        for (int row = 0; row < Vertexes.GetLength(1) - 1; row++)
        {
            for (int column = 0; column < Vertexes.GetLength(0) - 1; column++)
            {
                var voxel = Instantiate(_voxelPrefab, transform);
                voxel.name = $"Voxel ({column},{row})";
                
                voxel.Init(
                    Vertexes[column + 0, row + 0],
                    Vertexes[column + 0, row + 1],
                    Vertexes[column + 1, row + 1],
                    Vertexes[column + 1, row + 0]);
            }
        }
    }

    private Vector2 GetCenter()
    {
        var min = new Vector2(_x, _y);
        var max = new Vector2(_x + _width, _y + _height);
        return (min + max) / 2f;
    }
    
    public Bounds GetBounds()
    {
        var center = GetCenter();
        var size = new Vector3(_width, _height);
        return new Bounds(center, size);
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var voxel in Vertexes)
        {
            voxel.DrawGizmos();
        }

        var center = GetCenter();

        using (new GizmoColorScope(new Color(0f, 0f, 0f, 0.5f)))
        {
            Gizmos.DrawCube(center, new Vector3(_width, _height));
        }
    }
}