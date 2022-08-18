using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public BBD[,] mapdata;
    public BBD[,] mapitemdata;

    public Chunk()
    {
        mapdata = new BBD[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
        mapitemdata = new BBD[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];

        for (int y = 0; y < MapManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < MapManager.Instance.setting.chunkSize.x; x++)
            {
                mapdata[x, y].init();
                mapitemdata[x, y].init();
            }
        }
    }

    public void Writer(DataWriter dataWriter)
    {
        for (int y = 0; y < GameManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < GameManager.Instance.setting.chunkSize.x; x++)
            {
                dataWriter.Put(BitConverter.ToInt32(mapdata[x, y].data["id"]));
                dataWriter.Put(BitConverter.ToInt32(mapitemdata[x, y].data["id"]));
            }
        }
    }

    public void Reader(DataReader dataReader)
    {
        mapdata = new BBD[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
        mapitemdata = new BBD[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];

        for (int y = 0; y < MapManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < MapManager.Instance.setting.chunkSize.x; x++)
            {
                mapdata[x, y].init();
                mapitemdata[x, y].init();
            }
        }

        for (int y = 0; y < GameManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < GameManager.Instance.setting.chunkSize.x; x++)
            {
                mapdata[x, y].data["id"] = BitConverter.GetBytes(dataReader.GetInt());
                mapitemdata[x, y].data["id"] = BitConverter.GetBytes(dataReader.GetInt());
            }
        }
    }
}
