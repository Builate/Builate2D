using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct MapTile
{
    public string name;
    public Sprite icon;
    public TileBase tilebase;
    public bool hasCollider;
    public int Durability;
}
