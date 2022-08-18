using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    public Rigidbody2D rb2d;
    public Animator animator;
    public GameObject TileCursor;
    public float speed;
    public PlayerInventoryBox inventoryBox = new PlayerInventoryBox();
    public InventorySlot[] inventorySlots = new InventorySlot[9]; 
    public int handIndex;
    public Vector2Int moveDirection;
    public Vector2Int cursorDirection;
    public float interval;
    public float elapsed;

    void Start()
    {
        elapsed = interval;

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

        Vector2 velo = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (velo.magnitude >= 1)
        {
            velo.Normalize();
        }
        rb2d.velocity = velo * speed;

        SetMoveDirection();

        UpdateInventorySlotsOnselect();

        PlayerInput();

        SetCursorDirection();

        SetTileCursor();

        elapsed += Time.deltaTime;
        if (elapsed > interval) elapsed = interval + 1;

        // デバッグ
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
        {
            SaveManager.Instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftShift))
        {
            SaveManager.Instance.Load();
        }
    }

    public void SetMoveDirection()
    {
        Vector2 velo = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (velo.y < 0)
        {
            moveDirection.y = -1;

            if (velo.x == 0)
            {
                moveDirection.x = 0;
            }
        }
        else if (velo.y > 0)
        {
            moveDirection.y = 1;

            if (velo.x == 0)
            {
                moveDirection.x = 0;
            }
        }

        if (velo.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 0);
            moveDirection.x = -1;

            if (velo.y == 0)
            {
                moveDirection.y = 0;
            }
        }
        else if (velo.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            moveDirection.x = 1;

            if (velo.y == 0)
            {
                moveDirection.y = 0;
            }
        }
    }

    public void SetCursorDirection()
    {
        cursorDirection = moveDirection;

        Vector2 playerPos = transform.position;

        if (MapManager.Instance.map.TryGetValue(GameManager.Instance.GetChunkPosition(playerPos), out Chunk chunk))
        {
            Vector2Int pos = GameManager.Instance.GetTilePosition(playerPos);

            //コライダーを持っているなら
            if (GameManager.Instance.setting.mapItemTiles[System.BitConverter.ToInt32(chunk.mapitemdata[pos.x, pos.y].data["id"])].hasCollider)
            {
                cursorDirection = new Vector2Int(0, 0);
            }
        }
    }

    public void SetTileCursor()
    {
        TileCursor.transform.position = Vector3Int.FloorToInt(transform.position) + (Vector3Int)cursorDirection + new Vector3(.5f, .5f);
    }

    public void UpdateInventorySlotsOnselect()
    {
        for (int i = 0; i < 9; i++)
        {
            inventorySlots[i].onSelect = i == handIndex;
        }
    }

    public void PlayerInput()
    {
        // 壊す方
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.K))
        {
            Vector2 mousePos = (Vector2)transform.position + cursorDirection;

            if (elapsed > interval)
            {
                if (MapManager.Instance.map.TryGetValue(GameManager.Instance.GetChunkPosition(mousePos), out Chunk chunk))
                {
                    MapManager.Instance.SetTile(mousePos, 1, 0);
                    elapsed = 0;
                }
            }
        }

        // 置く方
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.L))
        {
            if (inventoryBox.PeekItem(handIndex, out int itemid, out int itemquantity))
            {
                var tilepos = (Vector2)transform.position + cursorDirection;

                if (MapManager.Instance.PlaceTile(itemid, tilepos))
                {
                    inventoryBox.GetItem(handIndex, out itemid);

                }
            }
        }
    }
}
