using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class InventorySlot : MonoBehaviour
{
    public int id;
    public Image image;
    public Action<int> onCkick;
    public bool onSelect;
    public GameObject Waku;

    public void LateUpdate()
    {
        Waku.SetActive(onSelect);
    }

    public void SetIcon(int id)
    {
        image.sprite = GameManager.Instance.setting.mapItemTiles[id].icon;
    }

    public void OnClick()
    {
        onCkick(id);
    }
}
