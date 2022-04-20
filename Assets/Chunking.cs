using UnityEngine;

public class Chunking : MonoBehaviour
{
    [SerializeField] private Chunk _chunkPrefab;

    private void Start()
    {
        var worldWidth = 5;
        var worldHeight = 7;
        var chunkWidth = 4;
        var chunkHeight = 4;

        for (int row = 0; row < worldHeight; row += chunkHeight)
        {
            for (int column = 0; column < worldWidth; column += chunkWidth)
            {
                var w = Mathf.Clamp(worldWidth - column, 1, chunkWidth);
                var h = Mathf.Clamp(worldHeight - row, 1, chunkHeight);
                Instantiate(_chunkPrefab).Init(column, row, w, h);
            }
        }
    }
}