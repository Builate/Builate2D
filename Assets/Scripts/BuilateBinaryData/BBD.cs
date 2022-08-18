using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BBD
{
    public Dictionary<string, object> data;

    public void Init()
    {
        data = new Dictionary<string, object>();
    }

    public int GetInt(string id)
    {
        return (int)data[id];
    }
}
