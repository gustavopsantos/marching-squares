using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Brush2 : MonoBehaviour
{
    public bool Enabled;
    public List<Chunk> Chunks = new List<Chunk>();
    public List<Chunk> Hovering = new List<Chunk>();
    [SerializeField] private Camera _camera;
    [SerializeField] private float _radius = 2;

    private Vector3 GetBrushCenter()
    {
        var point = _camera.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        return point;
    }

    private Bounds GetBrushBounds()
    {
        var center = GetBrushCenter();
        var size = new Vector3(_radius * 2, _radius * 2);
        return new Bounds(center, size);
    }

    // private void Update()
    // {
    //     var brushBounds = GetBrushBounds();
    //     Hovering = Chunks.Where(chunk => chunk.GetBounds().Intersects(brushBounds)).ToList();
    //     
    //     if (Input.GetKey(KeyCode.LeftAlt))
    //     {
    //         _radius += Input.GetAxisRaw("Mouse ScrollWheel");
    //     }
    //
    //     if (Input.GetKey(KeyCode.Mouse0))
    //     {
    //         foreach (var chunk in Hovering)
    //         {
    //             foreach (var vertex in chunk.Vertexes)
    //             {
    //                 var distance = Vector3.Distance(GetBrushCenter(), vertex.Center);
    //
    //                 if (distance > _radius)
    //                 {
    //                     continue;
    //                 }
    //
    //                 var normalizedDistance = distance / _radius;
    //                 var power = Mathf.Lerp(_increment, 0, normalizedDistance);
    //                 var framedIncrement = power * Time.deltaTime;
    //                 vertex.Value = Mathf.Clamp01(vertex.Value + framedIncrement);
    //
    //                 foreach (var voxel in vertex.Dependents)
    //                 {
    //                     _target.DirtyVoxels.Enqueue(voxel);
    //                 }
    //             }
    //         }
    //     }
    // }

    private void OnDrawGizmos()
    {
        var center = GetBrushCenter();
        Handles.DrawWireDisc(center, Vector3.back, _radius, 2);
    }
}