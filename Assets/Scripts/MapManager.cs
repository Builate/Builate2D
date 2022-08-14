using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    public Tilemap mapTilemap;
    public Tilemap mapItemTilemap;
    public TilemapCollider2D mapCollider;

    public Dictionary<Vector2Int, Chunk> map = new Dictionary<Vector2Int, Chunk>();
    public List<Vector2Int> LoadedChunks;
    public Setting setting;

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// �`�����N�����݂��Ȃ���ΐ�������
    /// </summary>
    /// <param name="chunkPosition"></param>
    /// <param name="t"></param>
    public void GenerateMap(Vector2Int chunkPosition, int t = 0)
    {
        if (!map.ContainsKey(chunkPosition))
        {
            FillMap(chunkPosition, t);
        }
    }

    /// <summary>
    /// �`�����N�̑��݂Ɋւ�炸��������
    /// </summary>
    /// <param name="chunkPosition"></param>
    /// <param name="t"></param>
    public void FillMap(Vector2Int chunkPosition, int t = 0)
    {
        Chunk chunk = new Chunk();

        for (int y = 0; y < setting.chunkSize.y; y++)
        {
            for (int x = 0; x < setting.chunkSize.x; x++)
            {
                Debug.Log(t);
                chunk.data[x, y] = t;
            }
        }

        map[chunkPosition] = chunk;
        if (!LoadedChunks.Contains(chunkPosition)) LoadedChunks.Add(chunkPosition);
    }

    public void SetTilemap(Vector2Int chunkPosition)
    {
        for (int y = 0; y < setting.chunkSize.y; y++)
        {
            for (int x = 0; x < setting.chunkSize.x; x++)
            {
                Vector3Int pos = new Vector3Int(x, y) + new Vector3Int(chunkPosition.x * setting.chunkSize.x, chunkPosition.y * setting.chunkSize.y);
                mapTilemap.SetTile(pos, setting.mapTiles[map[chunkPosition].data[x, y]].tilebase);

                PolygonCollider2D collider = new PolygonCollider2D();
            }
        }
    }
}
