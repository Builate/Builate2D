using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public Block[,] mapdata;
    public Block[,] mapitemdata;

    public void Init()
    {
        mapdata = new Block[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
        mapitemdata = new Block[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
    }

    public Chunk()
    {
        Init();
    }

    public void Writer(DataWriter dataWriter)
    {
        for (int y = 0; y < GameManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < GameManager.Instance.setting.chunkSize.x; x++)
            {
                dataWriter.Put(mapdata[x, y].id);
                dataWriter.Put(mapitemdata[x, y].id);
            }
        }
    }

    public void Reader(DataReader dataReader)
    {
        Init();

        for (int y = 0; y < GameManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < GameManager.Instance.setting.chunkSize.x; x++)
            {
                mapdata[x, y].id = dataReader.GetInt();
                mapitemdata[x, y].id = dataReader.GetInt();
            }
        }
    }
}
