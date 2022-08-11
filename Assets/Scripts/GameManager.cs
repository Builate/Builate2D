using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameObject Player;
    public Vector2Int l_playerpos;

    void Start()
    {
        
    }

    void Update()
    {
        if (GetChunkPosition(Player.transform.position) == l_playerpos)
        {

        }

        l_playerpos = GetChunkPosition(Player.transform.position);
    }

    public Vector2Int GetChunkPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x / 10f), Mathf.FloorToInt(position.y / 10f));
    }
}
