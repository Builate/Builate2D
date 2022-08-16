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

    /// <summary>
    /// チャンクが存在しなければ生成する
    /// </summary>
    /// <param name="chunkPosition"></param>
    /// <param name="t"></param>
    public Chunk GetMap(Vector2Int chunkPosition, int t = 0)
    {
        if (!map.ContainsKey(chunkPosition))
        {
            FillChunk(chunkPosition, t);
        }

        return map[chunkPosition];
    }

    /// <summary>
    /// チャンクの存在に関わらず生成する
    /// </summary>
    /// <param name="chunkPosition"></param>
    /// <param name="t"></param>
    public Chunk FillChunk(Vector2Int chunkPosition, int t = 0)
    {
        Chunk chunk = new Chunk();

        for (int y = 0; y < setting.chunkSize.y; y++)
        {
            for (int x = 0; x < setting.chunkSize.x; x++)
            {
                chunk.mapdata[x, y] = t;
                chunk.mapitemdata[x, y] = 0;
            }
        }

        map[chunkPosition] = chunk;
        if (!LoadedChunks.Contains(chunkPosition)) LoadedChunks.Add(chunkPosition);

        return chunk;
    }

    /// <summary>
    /// 指定したレイヤーにブロックを設置します。
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="layer"></param>
    /// <param name="id"></param>
    public void SetTile(Vector3 pos, int layer, int id)
    {
        Vector2Int chunkPos = GameManager.Instance.GetChunkPosition(new Vector3(pos.x, pos.y));
        Vector2Int tilePos = GameManager.Instance.GetTilePosition(pos);
        Chunk chunk = GetMap(chunkPos);

        switch (layer)
        {
            case 0:
                chunk.mapdata[tilePos.x, tilePos.y] = id;
                break;
            case 1:
                chunk.mapitemdata[tilePos.x, tilePos.y] = id;
                break;
            default:
                break;
        }

        SetTilemap(chunkPos, tilePos);
    }

    public void SetTilemap(Vector2Int chunkPosition)
    {
        for (int y = 0; y < setting.chunkSize.y; y++)
        {
            for (int x = 0; x < setting.chunkSize.x; x++)
            {
                SetTilemap(chunkPosition, new Vector2Int(x, y));
            }
        }
    }

    public void SetTilemap(Vector2Int chunkPosition, Vector2Int tilePosition)
    {
        Vector3Int pos = new Vector3Int(tilePosition.x, tilePosition.y) + new Vector3Int(chunkPosition.x * setting.chunkSize.x, chunkPosition.y * setting.chunkSize.y);
        mapTilemap.SetTile(pos, setting.mapTiles[map[chunkPosition].mapdata[tilePosition.x, tilePosition.y]].tilebase);
        mapItemTilemap.SetTile(pos, setting.mapItemTiles[map[chunkPosition].mapitemdata[tilePosition.x, tilePosition.y]].tilebase);
    }
}
