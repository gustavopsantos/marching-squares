using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Vertex
{
    public float Value;
    public Vector2 Center;
    public readonly List<Voxel> Dependents = new List<Voxel>();

    public Vertex(float value, Vector2 center)
    {
        Value = value;
        Center = center;
    }
    
    public static Vector2 Interpolate(Vertex a, Vertex b, float isoValue)
    {
        var normal = Mathf.InverseLerp(a.Value, b.Value, isoValue);
        return Vector2.Lerp(a.Center, b.Center, normal);
    }

    public void DrawGizmos()
    {
        Gizmos.DrawWireCube(Center, new Vector3(1, 1, 0));

        var style = new GUIStyle("label")
        {
            alignment = TextAnchor.MiddleCenter
        };

        Handles.Label(Center + Vector2.down * 0.1f, Value.ToString("0.0"), style);


        using (new GizmoColorScope(Color.Lerp(Color.black, Color.white, Value)))
        {
            Gizmos.DrawSphere(Center, 0.1f);
        }
    }
}