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
    public bool canPlace;
    public bool hasCollider;
    public bool isPickaxe;
    public int durability;
}
