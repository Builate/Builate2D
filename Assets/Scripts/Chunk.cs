using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public int[,] data;

    public Chunk()
    {
        data = new int[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
    }

    public void Writer(DataWriter dataWriter)
    {
        for (int y = 0; y < GameManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < GameManager.Instance.setting.chunkSize.x; x++)
            {
                dataWriter.Put(data[x, y]);
            }
        }
    }

    public void Reader(DataReader dataReader)
    {
        data = new int[MapManager.Instance.setting.chunkSize.x, MapManager.Instance.setting.chunkSize.y];
        for (int y = 0; y < GameManager.Instance.setting.chunkSize.y; y++)
        {
            for (int x = 0; x < GameManager.Instance.setting.chunkSize.x; x++)
            {
                data[x, y] = dataReader.GetInt();
            }
        }
    }
}
