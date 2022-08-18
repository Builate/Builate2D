using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameObject Player;
    public Vector2Int l_playerpos;

    public Setting setting;

    void Start()
    {
        try
        {
            SaveManager.Instance.Load();
        }
        catch (System.Exception ex)
        {

        }

        Vector2Int PlayerChunkPosition = GetChunkPosition(Player.transform.position);

        MapManager.Instance.GetMap(PlayerChunkPosition, 1);
        MapManager.Instance.SetTilemap(PlayerChunkPosition);
    }

    void Update()
    {
        // �����Ȃ��ꏊ�ɂ������蔻���t���邽�߂ɕK�v
        if (GetChunkPosition(Player.transform.position) != l_playerpos)
        {
            Vector2Int PlayerChunkPosition = GetChunkPosition(Player.transform.position);

            for (int y = 0; y < setting.loadMapSize.y; y++)
            {
                for (int x = 0; x < setting.loadMapSize.x; x++)
                {
                    Vector2Int locpos = new Vector2Int(x - setting.loadMapSize.x / 2, y - setting.loadMapSize.y / 2);
                    MapManager.Instance.GetMap(PlayerChunkPosition + locpos, 0);
                    MapManager.Instance.SetTilemap(PlayerChunkPosition + locpos);
                }
            }
        }

        l_playerpos = GetChunkPosition(Player.transform.position);
    }

    public Vector2Int GetChunkPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x / setting.chunkSize.x), Mathf.FloorToInt(position.y / setting.chunkSize.y));
    }

    public Vector2Int GetTilePosition(Vector3 pos)
    {
        Vector2Int chunkPos = GetChunkPosition(pos);
        return new Vector2Int(Mathf.FloorToInt(pos.x) - chunkPos.x * GameManager.Instance.setting.chunkSize.x, Mathf.FloorToInt(pos.y) - chunkPos.y * GameManager.Instance.setting.chunkSize.y);
    }
}
