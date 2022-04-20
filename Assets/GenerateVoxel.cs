using System;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateVoxel
{
    private static readonly Dictionary<int, Action<Voxel, float, ProceduralMesh>> _rebuild = new()
    {
        {0, RebuildConfiguration0},
        {1, RebuildConfiguration1},
        {2, RebuildConfiguration2},
        {3, RebuildConfiguration3},
        {4, RebuildConfiguration4},
        {5, RebuildConfiguration5},
        {6, RebuildConfiguration6},
        {7, RebuildConfiguration7},
        {8, RebuildConfiguration8},
        {9, RebuildConfiguration9},
        {10, RebuildConfiguration10},
        {11, RebuildConfiguration11},
        {12, RebuildConfiguration12},
        {13, RebuildConfiguration13},
        {14, RebuildConfiguration14},
        {15, RebuildConfiguration15},
    };

    private static readonly Dictionary<int, Action<Voxel, float, ProceduralMesh>> _update = new()
    {
        {0, UpdateConfiguration0},
        {1, UpdateConfiguration1},
        {2, UpdateConfiguration2},
        {3, UpdateConfiguration3},
        {4, UpdateConfiguration4},
        {5, UpdateConfiguration5},
        {6, UpdateConfiguration6},
        {7, UpdateConfiguration7},
        {8, UpdateConfiguration8},
        {9, UpdateConfiguration9},
        {10, UpdateConfiguration10},
        {11, UpdateConfiguration11},
        {12, UpdateConfiguration12},
        {13, UpdateConfiguration13},
        {14, UpdateConfiguration14},
        {15, UpdateConfiguration15},
    };

    // public static ProceduralMesh Generate(Voxel voxel, float isoValue)
    // {
    //     var contourKind = voxel.CalculateConfiguration(isoValue);
    //     var generator = _registry[contourKind];
    //     return generator.Invoke(voxel, isoValue);
    // }

    public static void Rebuild(Voxel voxel, float isoValue, byte configuration, ProceduralMesh procedural)
    {
        procedural.Vertices.Clear();
        procedural.Triangles.Clear();
        _rebuild[configuration].Invoke(voxel, isoValue, procedural);
    }

    public static void Update(Voxel voxel, float isoValue, byte configuration, ProceduralMesh procedural)
    {
        _update[configuration].Invoke(voxel, isoValue, procedural);
    }

    private static void RebuildConfiguration0(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
    }

    private static void UpdateConfiguration0(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
    }

    private static void RebuildConfiguration1(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[3]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2});
        UpdateConfiguration1(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration1(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[1] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[2] = voxel.GetBottomIntersection(isoValue);
    }

    private static void RebuildConfiguration2(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[3]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2});
        UpdateConfiguration2(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration2(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthEast.Center;
        proceduralMesh.Vertices[1] = voxel.GetBottomIntersection(isoValue);
        proceduralMesh.Vertices[2] = voxel.GetRightIntersection(isoValue);
    }

    private static void RebuildConfiguration3(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[6]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5});
        UpdateConfiguration3(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration3(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[1] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[2] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[3] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[4] = voxel.SouthEast.Center;
        proceduralMesh.Vertices[5] = voxel.SouthWest.Center;
    }

    private static void RebuildConfiguration4(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[3]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2});
        UpdateConfiguration4(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration4(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[1] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[2] = voxel.GetTopIntersection(isoValue);
    }

    private static void RebuildConfiguration5(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[12]);
        proceduralMesh.Triangles.AddRange(new int[]
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11
        });
        UpdateConfiguration5(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration5(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        var l = voxel.GetLeftIntersection(isoValue);
        var r = voxel.GetRightIntersection(isoValue);
        var b = voxel.GetBottomIntersection(isoValue);
        var t = voxel.GetTopIntersection(isoValue);
        var bl = voxel.SouthWest.Center;
        var tr = voxel.NorthEast.Center;

        proceduralMesh.Vertices[0] = bl;
        proceduralMesh.Vertices[1] = l;
        proceduralMesh.Vertices[2] = b;
        proceduralMesh.Vertices[3] = b;
        proceduralMesh.Vertices[4] = l;
        proceduralMesh.Vertices[5] = r;
        proceduralMesh.Vertices[6] = l;
        proceduralMesh.Vertices[7] = t;
        proceduralMesh.Vertices[8] = r;
        proceduralMesh.Vertices[9] = t;
        proceduralMesh.Vertices[10] = tr;
        proceduralMesh.Vertices[11] = r;
    }

    private static void RebuildConfiguration6(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[6]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5});
        UpdateConfiguration6(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration6(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.GetBottomIntersection(isoValue);
        proceduralMesh.Vertices[1] = voxel.GetTopIntersection(isoValue);
        proceduralMesh.Vertices[2] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[3] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[4] = voxel.SouthEast.Center;
        proceduralMesh.Vertices[5] = voxel.GetBottomIntersection(isoValue);
    }

    private static void RebuildConfiguration7(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[9]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8});
        UpdateConfiguration7(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration7(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthEast.Center;
        proceduralMesh.Vertices[1] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[2] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[3] = voxel.SouthEast.Center;
        proceduralMesh.Vertices[4] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[5] = voxel.GetTopIntersection(isoValue);
        proceduralMesh.Vertices[6] = voxel.SouthEast.Center;
        proceduralMesh.Vertices[7] = voxel.GetTopIntersection(isoValue);
        proceduralMesh.Vertices[8] = voxel.NorthEast.Center;
    }

    private static void RebuildConfiguration8(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[3]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2});
        UpdateConfiguration8(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration8(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[1] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[2] = voxel.GetTopIntersection(isoValue);
    }

    private static void RebuildConfiguration9(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[6]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5});
        UpdateConfiguration9(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration9(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[1] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[2] = voxel.GetTopIntersection(isoValue);
        proceduralMesh.Vertices[3] = voxel.GetTopIntersection(isoValue);
        proceduralMesh.Vertices[4] = voxel.GetBottomIntersection(isoValue);
        proceduralMesh.Vertices[5] = voxel.SouthWest.Center;
    }

    private static void RebuildConfiguration10(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[12]);
        proceduralMesh.Triangles.AddRange(new int[]
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11
        });
        UpdateConfiguration10(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration10(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        var l = voxel.GetLeftIntersection(isoValue);
        var r = voxel.GetRightIntersection(isoValue);
        var b = voxel.GetBottomIntersection(isoValue);
        var t = voxel.GetTopIntersection(isoValue);
        var br = voxel.SouthEast.Center;
        var tl = voxel.NorthWest.Center;

        proceduralMesh.Vertices[0] = br;
        proceduralMesh.Vertices[1] = b;
        proceduralMesh.Vertices[2] = r;
        proceduralMesh.Vertices[3] = b;
        proceduralMesh.Vertices[4] = l;
        proceduralMesh.Vertices[5] = r;
        proceduralMesh.Vertices[6] = r;
        proceduralMesh.Vertices[7] = l;
        proceduralMesh.Vertices[8] = t;
        proceduralMesh.Vertices[9] = l;
        proceduralMesh.Vertices[10] = tl;
        proceduralMesh.Vertices[11] = t;
    }

    private static void RebuildConfiguration11(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[9]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8});
        UpdateConfiguration11(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration11(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[1] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[2] = voxel.GetTopIntersection(isoValue);
        proceduralMesh.Vertices[3] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[4] = voxel.GetTopIntersection(isoValue);
        proceduralMesh.Vertices[5] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[6] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[7] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[8] = voxel.SouthEast.Center;
    }

    private static void RebuildConfiguration12(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[6]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5});
        UpdateConfiguration12(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration12(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[1] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[2] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[3] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[4] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[5] = voxel.GetLeftIntersection(isoValue);
    }

    private static void RebuildConfiguration13(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[9]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8});
        UpdateConfiguration13(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration13(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[1] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[2] = voxel.GetBottomIntersection(isoValue);
        proceduralMesh.Vertices[3] = voxel.GetBottomIntersection(isoValue);
        proceduralMesh.Vertices[4] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[5] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[6] = voxel.GetRightIntersection(isoValue);
        proceduralMesh.Vertices[7] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[8] = voxel.NorthEast.Center;
    }

    private static void RebuildConfiguration14(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[9]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8});
        UpdateConfiguration14(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration14(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[1] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[2] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[3] = voxel.GetLeftIntersection(isoValue);
        proceduralMesh.Vertices[4] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[5] = voxel.GetBottomIntersection(isoValue);
        proceduralMesh.Vertices[6] = voxel.GetBottomIntersection(isoValue);
        proceduralMesh.Vertices[7] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[8] = voxel.SouthEast.Center;
    }

    private static void RebuildConfiguration15(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices.AddRange(new Vector3[6]);
        proceduralMesh.Triangles.AddRange(new int[] {0, 1, 2, 3, 4, 5});
        UpdateConfiguration15(voxel, isoValue, proceduralMesh);
    }

    private static void UpdateConfiguration15(Voxel voxel, float isoValue, ProceduralMesh proceduralMesh)
    {
        proceduralMesh.Vertices[0] = voxel.SouthWest.Center;
        proceduralMesh.Vertices[1] = voxel.NorthWest.Center;
        proceduralMesh.Vertices[2] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[3] = voxel.NorthEast.Center;
        proceduralMesh.Vertices[4] = voxel.SouthEast.Center;
        proceduralMesh.Vertices[5] = voxel.SouthWest.Center;
    }
}