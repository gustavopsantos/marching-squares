using System;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateVoxel
{
    private static readonly Dictionary<int, Func<Voxel, float, ProceduralMesh>> _registry = new()
    {
        {0, GenerateConfiguration0},
        {1, GenerateConfiguration1},
        {2, GenerateConfiguration2},
        {3, GenerateConfiguration3},
        {4, GenerateConfiguration4},
        {5, GenerateConfiguration5},
        {6, GenerateConfiguration6},
        {7, GenerateConfiguration7},
        {8, GenerateConfiguration8},
        {9, GenerateConfiguration9},
        {10, GenerateConfiguration10},
        {11, GenerateConfiguration11},
        {12, GenerateConfiguration12},
        {13, GenerateConfiguration13},
        {14, GenerateConfiguration14},
        {15, GenerateConfiguration15},
    };

    public static ProceduralMesh Generate(Voxel voxel, float isoValue)
    {
        var contourKind = voxel.GetContourKind(isoValue);
        var generator = _registry[contourKind];
        return generator.Invoke(voxel, isoValue);
    }
    
    private static ProceduralMesh GenerateConfiguration0(Voxel voxel, float isoValue)
    {
        return ProceduralMesh.Empty;
    }

    private static ProceduralMesh GenerateConfiguration1(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthWest.Center,
            voxel.GetLeftIntersection(isoValue),
            voxel.GetBottomIntersection(isoValue)
        };

        int[] triangles =
        {
            0, 1, 2
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration2(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthEast.Center,
            voxel.GetBottomIntersection(isoValue),
            voxel.GetRightIntersection(isoValue)
        };

        int[] triangles =
        {
            0, 1, 2
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration3(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthWest.Center,
            voxel.GetLeftIntersection(isoValue),
            voxel.GetRightIntersection(isoValue),

            voxel.GetRightIntersection(isoValue),
            voxel.SouthEast.Center,
            voxel.SouthWest.Center
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration4(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.NorthEast.Center,
            voxel.GetRightIntersection(isoValue),
            voxel.GetTopIntersection(isoValue)
        };

        int[] triangles =
        {
            0, 1, 2
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration5(Voxel voxel, float isoValue)
    {
        var l = voxel.GetLeftIntersection(isoValue);
        var r = voxel.GetRightIntersection(isoValue);
        var b = voxel.GetBottomIntersection(isoValue);
        var t = voxel.GetTopIntersection(isoValue);
        var bl = voxel.SouthWest.Center;
        var tr = voxel.NorthEast.Center;

        Vector3[] vertices =
        {
            bl, l, b,
            b, l, r,
            l, t, r,
            t, tr, r
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration6(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.GetBottomIntersection(isoValue),
            voxel.GetTopIntersection(isoValue),
            voxel.NorthEast.Center,

            voxel.NorthEast.Center,
            voxel.SouthEast.Center,
            voxel.GetBottomIntersection(isoValue)
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration7(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthEast.Center,
            voxel.SouthWest.Center,
            voxel.GetLeftIntersection(isoValue),

            voxel.SouthEast.Center,
            voxel.GetLeftIntersection(isoValue),
            voxel.GetTopIntersection(isoValue),

            voxel.SouthEast.Center,
            voxel.GetTopIntersection(isoValue),
            voxel.NorthEast.Center
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration8(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.GetLeftIntersection(isoValue),
            voxel.NorthWest.Center,
            voxel.GetTopIntersection(isoValue)
        };

        int[] triangles =
        {
            0, 1, 2
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration9(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthWest.Center,
            voxel.NorthWest.Center,
            voxel.GetTopIntersection(isoValue),

            voxel.GetTopIntersection(isoValue),
            voxel.GetBottomIntersection(isoValue),
            voxel.SouthWest.Center
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration10(Voxel voxel, float isoValue)
    {
        var l = voxel.GetLeftIntersection(isoValue);
        var r = voxel.GetRightIntersection(isoValue);
        var b = voxel.GetBottomIntersection(isoValue);
        var t = voxel.GetTopIntersection(isoValue);
        var br = voxel.SouthEast.Center;
        var tl = voxel.NorthWest.Center;

        Vector3[] vertices =
        {
            br, b, r,
            b, l, r,
            r, l, t,
            l, tl, t
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration11(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthWest.Center,
            voxel.NorthWest.Center,
            voxel.GetTopIntersection(isoValue),

            voxel.SouthWest.Center,
            voxel.GetTopIntersection(isoValue),
            voxel.GetRightIntersection(isoValue),

            voxel.SouthWest.Center,
            voxel.GetRightIntersection(isoValue),
            voxel.SouthEast.Center
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration12(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.GetLeftIntersection(isoValue),
            voxel.NorthWest.Center,
            voxel.NorthEast.Center,

            voxel.NorthEast.Center,
            voxel.GetRightIntersection(isoValue),
            voxel.GetLeftIntersection(isoValue)
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration13(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthWest.Center,
            voxel.NorthWest.Center,
            voxel.GetBottomIntersection(isoValue),

            voxel.GetBottomIntersection(isoValue),
            voxel.NorthWest.Center,
            voxel.GetRightIntersection(isoValue),

            voxel.GetRightIntersection(isoValue),
            voxel.NorthWest.Center,
            voxel.NorthEast.Center
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration14(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.GetLeftIntersection(isoValue),
            voxel.NorthWest.Center,
            voxel.NorthEast.Center,

            voxel.GetLeftIntersection(isoValue),
            voxel.NorthEast.Center,
            voxel.GetBottomIntersection(isoValue),

            voxel.GetBottomIntersection(isoValue),
            voxel.NorthEast.Center,
            voxel.SouthEast.Center
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8
        };

        return new ProceduralMesh(vertices, triangles);
    }

    private static ProceduralMesh GenerateConfiguration15(Voxel voxel, float isoValue)
    {
        Vector3[] vertices =
        {
            voxel.SouthWest.Center,
            voxel.NorthWest.Center,
            voxel.NorthEast.Center,

            voxel.NorthEast.Center,
            voxel.SouthEast.Center,
            voxel.SouthWest.Center
        };

        int[] triangles =
        {
            0, 1, 2,
            3, 4, 5
        };

        return new ProceduralMesh(vertices, triangles);
    }
}