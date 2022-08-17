using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InventoryTile
{
    public int id;
    public int quantity;

    public void Writer(DataWriter writer)
    {
        writer.Put(id);
        writer.Put(quantity);
    }

    public void Reader(DataReader reader)
    {
        id = reader.GetInt();
        quantity = reader.GetInt();
    }
}
