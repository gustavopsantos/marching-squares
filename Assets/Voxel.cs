using UnityEditor;
using UnityEngine;

public class Voxel : MonoBehaviour
{
    private Vertex _southWest; // bottom left
    private Vertex _northWest; // top left
    private Vertex _northEast; // top right
    private Vertex _southEast; // bottom right

    private float TopLeftValue => _northWest.Value;
    private float BottomLeftValue => _southWest.Value;
    private float TopRightValue => _northEast.Value;
    private float BottomRightValue => _southEast.Value;

    public Vector2 TopLeftPoint => _northWest.Center;
    public Vector2 BottomLeftPoint => _southWest.Center;
    public Vector2 TopRightPoint => _northEast.Center;
    public Vector2 BottomRightPoint => _southEast.Center;

    public void Init(Vertex southWest, Vertex northWest, Vertex northEast, Vertex southEast)
    {
        _southWest = southWest;
        _northWest = northWest;
        _northEast = northEast;
        _southEast = southEast;
    }

    public int GetContourKind(float isoValue)
    {
        var configuration = 0;
        if (_southWest.Value > isoValue) configuration += 1; // South west
        if (_southEast.Value > isoValue) configuration += 2; // South east
        if (_northEast.Value > isoValue) configuration += 4; // North east
        if (_northWest.Value > isoValue) configuration += 8; // North west
        return configuration;
    }

    public Vector2 GetLeftIntersection(float isoValue)
    {
        var leftIntersectionValue = Mathf.InverseLerp(TopLeftValue, BottomLeftValue, isoValue);
        return Vector2.Lerp(TopLeftPoint, BottomLeftPoint, leftIntersectionValue);
    }

    public Vector2 GetRightIntersection(float isoValue)
    {
        var rightIntersectionValue = Mathf.InverseLerp(TopRightValue, BottomRightValue, isoValue);
        return Vector2.Lerp(TopRightPoint, BottomRightPoint, rightIntersectionValue);
    }

    public Vector2 GetBottomIntersection(float isoValue)
    {
        var bottomIntersectionValue = Mathf.InverseLerp(BottomLeftValue, BottomRightValue, isoValue);
        return Vector2.Lerp(BottomLeftPoint, BottomRightPoint, bottomIntersectionValue);
    }

    public Vector2 GetTopIntersection(float isoValue)
    {
        var topIntersectionValue = Mathf.InverseLerp(TopRightValue, TopLeftValue, isoValue);
        return Vector2.Lerp(TopRightPoint, TopLeftPoint, topIntersectionValue);
    }


    public void OnDrawGizmosSelected()
    {
        var center = (_southWest.Center + _northEast.Center) / 2;
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
            Gizmos.DrawWireSphere(_southWest.Center, 0.2f);
        }

        using (new GizmoColorScope(Color.green))
        {
            Gizmos.DrawWireSphere(_northWest.Center, 0.2f);
        }

        using (new GizmoColorScope(Color.blue))
        {
            Gizmos.DrawWireSphere(_northEast.Center, 0.2f);
        }

        using (new GizmoColorScope(Color.white))
        {
            Gizmos.DrawWireSphere(_southEast.Center, 0.2f);
        }
    }
}