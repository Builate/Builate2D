using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    public Rigidbody2D rb2d;
    public Animator animator;
    public float speed;
    public PlayerInventoryBox inventoryBox = new PlayerInventoryBox();
    public InventorySlot[] inventorySlots = new InventorySlot[9]; 
    public int handIndex;

    void Start()
    {
        foreach (var item in inventorySlots)
        {
            item.onCkick = i =>
            {
                handIndex = i;
            };
        }

        inventoryBox.onChange = () =>
        {
            for (int i = 0; i < 9; i++)
            {
                if (inventoryBox.PeekItem(i, out int itemid, out int itemquantity))
                {
                    inventorySlots[i].SetIcon(itemid);
                }
            }
        };
    }

    void Update()
    {
        animator.SetBool("isWalk", rb2d.velocity != Vector2.zero);

        rb2d.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        if (rb2d.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        else if(rb2d.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

        for (int i = 0; i < 9; i++)
        {
            inventorySlots[i].onSelect = i == handIndex;
        }


        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int tilepos = GameManager.Instance.GetTilePosition(mousePos);

            if (MapManager.Instance.map.TryGetValue(GameManager.Instance.GetChunkPosition(mousePos), out Chunk chunk))
            {
                MapManager.Instance.SetTile(mousePos, 1, 0);
            }
        }

        if (Input.GetMouseButton(1))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int tilepos = GameManager.Instance.GetTilePosition(mousePos);

            if (MapManager.Instance.map.TryGetValue(GameManager.Instance.GetChunkPosition(mousePos), out Chunk chunk))
            {
                if (chunk.mapitemdata[tilepos.x,tilepos.y] == 0)
                {
                    if (chunk.mapdata[tilepos.x,tilepos.y] != 0)
                    {
                        if (inventoryBox.GetItem(handIndex, out int itemid))
                        {
                            MapManager.Instance.SetTile(mousePos, 1, itemid);
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.Instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.Instance.Load();
        }
    }
}
