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
}
