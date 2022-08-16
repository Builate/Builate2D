using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInventoryBox : InventoryBox
{
    public InventoryTile[] items = new InventoryTile[9];

    public override bool PeekItem(int index, out int itemid, out int itemquantity)
    {
        itemid = 0;
        itemquantity = 0;

        // index‚ª”ÍˆÍŠO‚Ìê‡return‚·‚é
        if (items.Length < index || 0 > index) return false;

        itemid = items[index].id;
        itemquantity = items[index].quantity;
        return true;
    }

    public override bool GetItem(int index, out int itemid)
    {
        itemid = 0;

        // index‚ª”ÍˆÍŠO‚Ìê‡return‚·‚é
        if (items.Length < index || 0 > index) return false;

        // quantity‚ª0ˆÈ‰º‚Ìê‡return‚·‚é
        if (items[index].quantity <= 0) return false;

        // quantity‚ğ1Œ¸‚ç‚µ‚Äreturn‚·‚é
        items[index].quantity--;
        itemid = items[index].id;

        if (items[index].quantity == 0) items[index].id = 0;
        return true;
    }

    public override bool AddItem(int index, int itemid)
    {
        // index‚ª”ÍˆÍŠO‚Ìê‡return‚·‚é
        if (items.Length < index || 0 > index) return false;

        // id ‚ªˆê’v‚µ‚È‚¢ê‡return‚·‚é
        if (items[index].quantity > 0 && items[index].id != itemid) return false;

        items[index].quantity++;
        items[index].id = itemid;
        return true;
    }
}