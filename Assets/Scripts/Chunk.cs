using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public int[,] mapdata;
    public int[,] mapitemdata;

    public BBD[,] mapBBD;
    public BBD[,] mapitemBBD;

    public void Init()
    {
        mapdata = new int[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
        mapitemdata = new int[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
        mapBBD = new BBD[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
        mapitemBBD = new BBD[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];

        for (int y = 0; y < MapManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < MapManager.Instance.setting.chunkSize.x; x++)
            {
                mapBBD[x, y] = new BBD();
                mapitemBBD[x, y] = new BBD();
            }
        }
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
                dataWriter.Put(mapdata[x, y]);
                dataWriter.Put(mapitemdata[x, y]);
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
                mapdata[x, y] = dataReader.GetInt();
                mapitemdata[x, y] = dataReader.GetInt();
            }
        }
    }
}
