using UnityEditor;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField] private float _radius = 4;
    [SerializeField] private World _world;
    [SerializeField] private Meshing _meshing;
    [SerializeField] private Camera _camera;
    [SerializeField, Range(-16, +16)] private float _increment = 16;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            _radius += Input.GetAxisRaw("Mouse ScrollWheel");
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            foreach (var vertex in _world.Vertexes)
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

    private Vector3 GetBrushCenter()
    {
        var point = _camera.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        return point;
    }

    private void OnDrawGizmos()
    {
        var point = _camera.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        Gizmos.DrawSphere(point, 0.1f);
        Handles.DrawWireDisc(point, Vector3.back, _radius, 2);
    }
}