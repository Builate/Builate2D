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

    public float _handIndex;
    public int handIndex
    {
        get
        {
            return (int)_handIndex;
        }
    }
    public Vector2Int moveDirection;
    public Vector2Int cursorPos;
    public float interval;
    public float elapsed;

    void Start()
    {
        foreach (var item in inventorySlots)
        {
            item.onCkick = i =>
            {
                _handIndex = i;
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


        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Vector2Int chunkPos = new Vector2Int(x - 3 / 2, y - 3 / 2);
                MapManager.Instance.GetMap(chunkPos, 1);
                MapManager.Instance.SetTilemap(chunkPos);
            }
        }
        for (int i = 0; i < 100; i++)
        {
            inventoryBox.AddItem(0, 1);
        }
        for (int i = 0; i < 100; i++)
        {
            inventoryBox.AddItem(1, 2);
        }
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


        // �f�o�b�O
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKey(KeyCode.LeftShift))
        {
            SaveManager.Instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftShift))
        {
            SaveManager.Instance.Load();
        }

        elapsed += Time.deltaTime;
        if (elapsed > interval)
        {
            elapsed = interval + 1;
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

            moveDirection.y = 0;
        }
        else if (velo.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            moveDirection.x = 1;

            moveDirection.y = 0;
        }
    }

    public void SetCursorDirection()
    {
        Vector2Int p = Vector2Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector2Int playerPos = Vector2Int.FloorToInt(transform.position);
        p = new Vector2Int(Mathf.Clamp(p.x, -1 + playerPos.x, 1 + playerPos.x), Mathf.Clamp(p.y, -1 + playerPos.y, 1 + playerPos.y));

        cursorPos = p;
    }

    public void SetTileCursor()
    {
        TileCursor.transform.position = (Vector3Int)cursorPos + new Vector3(.5f, .5f);
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
        // 破壊
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = cursorPos;
            Vector2Int _mousePos = GameManager.Instance.GetTilePosition(mousePos);

            if (elapsed > interval)
            {
                if (inventoryBox.PeekItem(handIndex, out int itemid, out int itemquantity))
                {
                    if (GameManager.Instance.setting.mapItemTiles[itemid].isPickaxe)
                    {
                        if (MapManager.Instance.map.TryGetValue(GameManager.Instance.GetChunkPosition(mousePos), out Chunk chunk))
                        {
                            /*
                            Debug.Log(chunk.mapitemBBD[_mousePos.x, _mousePos.y].GetInt("damage"));
                            chunk.mapitemBBD[_mousePos.x, _mousePos.y].data["damage"] = chunk.mapitemBBD[_mousePos.x, _mousePos.y].GetInt("damage") + 1;

                            if (chunk.mapitemBBD[_mousePos.x, _mousePos.y].GetInt("damage") >= GameManager.Instance.setting.mapItemTiles[chunk.mapitemdata[_mousePos.x, _mousePos.y]].durability) 
                            {
                                MapManager.Instance.SetTile(mousePos, 1, 0);
                            }
                            */
                            elapsed = 0;
                        }
                    }
                }
            }
        }

        // 設置
        if (Input.GetMouseButton(1)) 
        {
            if (inventoryBox.PeekItem(handIndex, out int itemid, out int itemquantity))
            {
                if (GameManager.Instance.setting.mapItemTiles[itemid].canPlace)
                {
                    Vector2 tilepos = cursorPos;

                    if (MapManager.Instance.DestroyTile(itemid, tilepos))
                    {
                        inventoryBox.GetItem(handIndex, out itemid);
                    }
                }
            }
        }

        // 持ち物変更
        float mousewheel = -Input.GetAxisRaw("Mouse ScrollWheel");
        _handIndex += (mousewheel + 9) * GameManager.Instance.setting.mouseWheelSensi;

        _handIndex %= 9;
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
