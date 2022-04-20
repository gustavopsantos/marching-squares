using System.Collections.Generic;
using UnityEngine;

public class Meshing : MonoBehaviour
{
    [field: SerializeField, Range(0, 1)] public float IsoValue { get; private set; } = 0.01f;
    
    private readonly Queue<Voxel> _registry = new ();

    public void Enqueue(Voxel voxel)
    {
        _registry.Enqueue(voxel);
    }
    
    private void LateUpdate()
    {
        while (_registry.TryDequeue(out var dirtyVoxel))
        {
            dirtyVoxel.Rebuild(IsoValue);
        }
    }
}
