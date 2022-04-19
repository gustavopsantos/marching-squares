using UnityEditor;
using UnityEngine;

public class Brush : MonoBehaviour
{
    [SerializeField] private float _radius = 1;
    [SerializeField] private World _target;
    [SerializeField] private Camera _camera;
    [SerializeField, Range(-2, +2)] private float _increment = 1;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            _radius += Input.GetAxisRaw("Mouse ScrollWheel");
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            foreach (var voxel in _target.Vertexes)
            {
                var distance = Vector3.Distance(GetBrushCenter(), voxel.Center);
                var normalizedDistance = distance / _radius;
                var power = Mathf.Lerp(_increment, 0, normalizedDistance);
                var framedIncrement = power * Time.deltaTime;
                voxel.Value = Mathf.Clamp01(voxel.Value + framedIncrement);
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