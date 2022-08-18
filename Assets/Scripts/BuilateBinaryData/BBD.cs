using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BBD
{
    public Dictionary<string, byte[]> data;

    public void init()
    {
        data = new Dictionary<string, byte[]>();
    }
}
