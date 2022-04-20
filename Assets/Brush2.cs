using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Brush2 : MonoBehaviour
{
    [SerializeField] private Chunking _chunking;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _radius = 4;
    [SerializeField] private Meshing _meshing;
    [SerializeField, Range(-16, +16)] private float _increment = 16;

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            _radius += Input.GetAxisRaw("Mouse ScrollWheel");
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var brushBounds = GetBrushBounds();
            var hovering = _chunking.Chunks.Where(chunk => chunk.GetBounds().Intersects(brushBounds));
            
            foreach (var chunk in hovering)
            {
                foreach (var vertex in chunk.Vertexes)
                {
                    var distance = Vector3.Distance(GetBrushCenter(), vertex.Center);

                    if (distance > _radius)
                    {
                        continue;
                    }

                    var normalizedDistance = distance / _radius;
                    var power = Mathf.Lerp(_increment, 0, normalizedDistance);
                    var framedIncrement = power * Time.deltaTime;
                    vertex.Value = Mathf.Clamp01(vertex.Value + framedIncrement);

                    foreach (var voxel in vertex.Dependents)
                    {
                        _meshing.Enqueue(voxel);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        var center = GetBrushCenter();
        Handles.DrawWireDisc(center, Vector3.back, _radius, 2);
    }
}