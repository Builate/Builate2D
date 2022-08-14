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
        SaveManager.Instance.Load();

        Vector2Int PlayerChunkPosition = GetChunkPosition(Player.transform.position);

        MapManager.Instance.GenerateMap(PlayerChunkPosition, 1);
        MapManager.Instance.SetTilemap(PlayerChunkPosition);
    }

    void Update()
    {
        // ‰½‚à‚È‚¢êŠ‚É‚à“–‚½‚è”»’è‚ğ•t‚¯‚é‚½‚ß‚É•K—v
        if (GetChunkPosition(Player.transform.position) != l_playerpos)
        {
            Vector2Int PlayerChunkPosition = GetChunkPosition(Player.transform.position);

            for (int y = 0; y < setting.loadMapSize.y; y++)
            {
                for (int x = 0; x < setting.loadMapSize.x; x++)
                {
                    Vector2Int locpos = new Vector2Int(x - setting.loadMapSize.x / 2, y - setting.loadMapSize.y / 2);
                    MapManager.Instance.GenerateMap(PlayerChunkPosition + locpos, 0);
                    MapManager.Instance.SetTilemap(PlayerChunkPosition + locpos);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2Int cursorChunkPosition = GetChunkPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            MapManager.Instance.FillMap(cursorChunkPosition, 1);
            MapManager.Instance.SetTilemap(cursorChunkPosition);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.Instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.Instance.Load();
        }

        l_playerpos = GetChunkPosition(Player.transform.position);
    }

    public Vector2Int GetChunkPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x / setting.chunkSize.x), Mathf.FloorToInt(position.y / setting.chunkSize.y));
    }
}
