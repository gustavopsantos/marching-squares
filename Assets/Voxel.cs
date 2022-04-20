using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Voxel : MonoBehaviour
{
    private ProceduralMesh _procedural;
    private MeshFilter _meshFilter;

    public Vertex SouthWest { get; private set; } // bottom left
    public Vertex NorthWest { get; private set; } // top left
    public Vertex NorthEast { get; private set; } // top right
    public Vertex SouthEast { get; private set; } // bottom right

    public Observable<byte> Configuration { get; } = new Observable<byte>(0);

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

        _procedural = new ProceduralMesh();
    }

    public byte CalculateConfiguration(float isoValue)
    {
        byte configuration = 0;
        if (SouthWest.Value > isoValue) configuration += 1; // South west
        if (SouthEast.Value > isoValue) configuration += 2; // South east
        if (NorthEast.Value > isoValue) configuration += 4; // North east
        if (NorthWest.Value > isoValue) configuration += 8; // North west

        Configuration.Value = configuration;
        return configuration;
    }

    public Vector2 GetLeftIntersection(float isoValue)
    {
        var leftIntersectionValue = Mathf.InverseLerp(NorthWest.Value, SouthWest.Value, isoValue);
        return Vector2.Lerp(NorthWest.Center, SouthWest.Center, leftIntersectionValue);
    }

    public Vector2 GetRightIntersection(float isoValue)
    {
        var rightIntersectionValue = Mathf.InverseLerp(NorthEast.Value, SouthEast.Value, isoValue);
        return Vector2.Lerp(NorthEast.Center, SouthEast.Center, rightIntersectionValue);
    }

    public Vector2 GetBottomIntersection(float isoValue)
    {
        var bottomIntersectionValue = Mathf.InverseLerp(SouthWest.Value, SouthEast.Value, isoValue);
        return Vector2.Lerp(SouthWest.Center, SouthEast.Center, bottomIntersectionValue);
    }

    public Vector2 GetTopIntersection(float isoValue)
    {
        var topIntersectionValue = Mathf.InverseLerp(NorthEast.Value, NorthWest.Value, isoValue);
        return Vector2.Lerp(NorthEast.Center, NorthWest.Center, topIntersectionValue);
    }

    private void Start()
    {
        _meshFilter = gameObject.GetComponent<MeshFilter>();
        _meshFilter.sharedMesh = _procedural.Mesh;
    }
    
    public void UpdateProceduralMesh(float isoValue, byte configuration)
    {
        GenerateVoxel.Update(this, isoValue, configuration, _procedural);
        _procedural.Build();
    }

    public void RebuildProceduralMesh(float isoValue, byte configuration)
    {
        GenerateVoxel.Rebuild(this, isoValue, configuration, _procedural);
        _procedural.Build();

        //
        //
        // var replacement = GenerateVoxel.Generate(this, isoValue);
        // replacement.Build();
        // _meshFilter.sharedMesh = replacement.Mesh;
    }

    public void OnDrawGizmosSelected()
    {
        var center = (SouthWest.Center + NorthEast.Center) / 2;
        Gizmos.DrawWireCube(center, new Vector3(1, 1));

        var style = new GUIStyle("label")
        {
            alignment = TextAnchor.MiddleCenter
        };

        var isoValue = GetComponentInParent<Meshing>().IsoValue;
        Handles.Label(center + Vector2.up * 0.1f, CalculateConfiguration(isoValue).ToString(), style);

        using (new GizmoColorScope(Color.yellow))
        {
            var gizmoSize = new Vector3(0.1f, 0.1f);
            var l = GetLeftIntersection(isoValue);
            var r = GetRightIntersection(isoValue);
            var t = GetTopIntersection(isoValue);
            var b = GetBottomIntersection(isoValue);
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