using UnityEngine;
using System.Collections.Generic;

public class InfiniteTileGenerator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform tileParent;
    [SerializeField] private int radius = 10;

    private Dictionary<Vector2Int, GameObject> _spawnedTiles = new();

    void Update()
    {
        GenerateAroundPlayer();
    }

    void GenerateAroundPlayer()
    {
        Vector2 playerPos = player.position;

        int playerX = Mathf.FloorToInt(playerPos.x);
        int playerY = Mathf.FloorToInt(playerPos.y);

        for (int x = playerX - radius; x <= playerX + radius; x++)
        {
            for (int y = playerY - radius; y <= playerY + radius; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);

                if (_spawnedTiles.ContainsKey(cell)) continue;
                GameObject tile = Instantiate(tilePrefab, tileParent);
                tile.transform.position = new Vector2(x, y);
                _spawnedTiles[cell] = tile;
            }
        }
    }
}