using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryBox
{
    public abstract bool PeekItem(int index, out int itemid, out int itemquantity);

    public abstract bool GetItem(int index, out int itemid);

    public abstract bool AddItem(int index, int itemid);

    public abstract void Writer(DataWriter writer);

    public abstract void Reader(DataReader reader);
}
