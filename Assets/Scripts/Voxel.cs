using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Voxel : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;

    public Vertex SouthWest { get; private set; } // bottom left
    public Vertex NorthWest { get; private set; } // top left
    public Vertex NorthEast { get; private set; } // top right
    public Vertex SouthEast { get; private set; } // bottom right

    public void Init(Vertex southWest, Vertex northWest, Vertex northEast, Vertex southEast)
    {
        SouthWest = southWest;
        NorthWest = northWest;
        NorthEast = northEast;
        SouthEast = southEast;
        
        SouthWest.Dependents.Add(this);
        NorthWest.Dependents.Add(this);
        NorthEast.Dependents.Add(this);
        SouthEast.Dependents.Add(this);
    }

    public int GetContourKind(float isoValue)
    {
        var configuration = 0;
        if (SouthWest.Value > isoValue) configuration += 1; // South west
        if (SouthEast.Value > isoValue) configuration += 2; // South east
        if (NorthEast.Value > isoValue) configuration += 4; // North east
        if (NorthWest.Value > isoValue) configuration += 8; // North west
        return configuration;
    }

    public Vector2 GetLeftEdgePoint(float isoValue)
    {
        return Vertex.Interpolate(NorthWest, SouthWest, isoValue);
    }

    public Vector2 GetRightEdgePoint(float isoValue)
    {
        return Vertex.Interpolate(NorthEast, SouthEast, isoValue);
    }

    public Vector2 GetBottomEdgePoint(float isoValue)
    {
        return Vertex.Interpolate(SouthWest, SouthEast, isoValue);
    }

    public Vector2 GetTopEdgePoint(float isoValue)
    {
        return Vertex.Interpolate(NorthEast, NorthWest, isoValue);
    }

    public void RegenerateMesh(float isoValue)
    {
        _meshFilter.sharedMesh = GenerateVoxel.Generate(this, isoValue).Mesh;
    }

    public void OnDrawGizmosSelected()
    {
        var center = (SouthWest.Center + NorthEast.Center) / 2;
        Gizmos.DrawWireCube(center, new Vector3(1, 1));

        var style = new GUIStyle("label")
        {
            alignment = TextAnchor.MiddleCenter
        };

        var isoValue = FindObjectOfType<World>().IsoValue;
        Handles.Label(center + Vector2.up * 0.1f, GetContourKind(isoValue).ToString(), style);

        using (new GizmoColorScope(Color.yellow))
        {
            var gizmoSize = new Vector3(0.1f, 0.1f);
            var l = GetLeftEdgePoint(isoValue);
            var r = GetRightEdgePoint(isoValue);
            var t = GetTopEdgePoint(isoValue);
            var b = GetBottomEdgePoint(isoValue);
            Gizmos.DrawCube(l, gizmoSize);
            Gizmos.DrawCube(r, gizmoSize);
            Gizmos.DrawCube(t, gizmoSize);
            Gizmos.DrawCube(b, gizmoSize);
        }

        using (new GizmoColorScope(Color.red))
        {
            Gizmos.DrawWireSphere(SouthWest.Center, 0.2f);
        }

        using (new GizmoColorScope(Color.green))
        {
            Gizmos.DrawWireSphere(NorthWest.Center, 0.2f);
        }

        using (new GizmoColorScope(Color.blue))
        {
            Gizmos.DrawWireSphere(NorthEast.Center, 0.2f);
        }

        using (new GizmoColorScope(Color.white))
        {
            Gizmos.DrawWireSphere(SouthEast.Center, 0.2f);
        }
    }
}