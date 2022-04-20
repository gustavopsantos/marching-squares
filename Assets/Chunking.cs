using System.Collections.Generic;
using UnityEngine;

public class Chunking : MonoBehaviour
{
    [SerializeField] private Chunk _chunkPrefab;

    public readonly List<Chunk> Chunks = new List<Chunk>();

    private void Start()
    {
        var worldWidth = 128;
        var worldHeight = 128;
        var chunkWidth = 64;
        var chunkHeight = 64;

        for (int row = 0; row < worldHeight; row += chunkHeight)
        {
            for (int column = 0; column < worldWidth; column += chunkWidth)
            {
                var w = Mathf.Clamp(worldWidth - column, 1, chunkWidth);
                var h = Mathf.Clamp(worldHeight - row, 1, chunkHeight);
                var chunk = Instantiate(_chunkPrefab, transform);
                chunk.Init(column, row, w, h);
                Chunks.Add(chunk);
            }
        }
    }
}