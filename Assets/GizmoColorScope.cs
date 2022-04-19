using System;
using UnityEngine;

public class GizmoColorScope : IDisposable
{
    private readonly Color _previous;

    public GizmoColorScope(Color color)
    {
        _previous = Gizmos.color;
        Gizmos.color = color;
    }

    public void Dispose()
    {
        Gizmos.color = _previous;
    }
}