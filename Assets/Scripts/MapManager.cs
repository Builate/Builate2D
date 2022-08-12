using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    public Tilemap mapTilemap;
    public Tilemap mapItemTilemap;

    public Dictionary<Vector2Int, Chunk> map = new Dictionary<Vector2Int, Chunk>();
    public List<Vector2Int> LoadedChunks;
    public Setting setting;

    void Start()
    {

    }

    void Update()
    {

    }

    public void GenerateMap(Vector2Int chunkPosition)
    {
        if (!map.ContainsKey(chunkPosition))
        {
            Chunk chunk = new Chunk();

            for (int y = 0; y < setting.chunkSize.y; y++)
            {
                for (int x = 0; x < setting.chunkSize.x; x++)
                {
                    chunk.data[x, y] = 3;
                }
            }

            map.Add(chunkPosition, chunk);
            LoadedChunks.Add(chunkPosition);
        }
    }

    public void SetTilemap(Vector2Int chunkPosition)
    {
        for (int y = 0; y < setting.chunkSize.y; y++)
        {
            for (int x = 0; x < setting.chunkSize.x; x++)
            {
                mapTilemap.SetTile(new Vector3Int(x, y) + new Vector3Int(chunkPosition.x * setting.chunkSize.x, chunkPosition.y * setting.chunkSize.y), setting.mapTiles[map[chunkPosition].data[x, y]].tilebase);
            }
        }
    }
}
