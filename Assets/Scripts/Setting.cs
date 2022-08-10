using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Setting : ScriptableObject
{
    public List<MapTile> mapTiles = new List<MapTile>();
    public Vector2Int chunkSize;
}
