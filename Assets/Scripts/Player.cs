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


        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Vector2Int chunkPos = new Vector2Int(x - 3 / 2, y - 3 / 2);
                MapManager.Instance.FillChunk(chunkPos, 1);
                MapManager.Instance.SetTilemap(chunkPos);
            }
        }
        for (int i = 0; i < 100; i++)
        {
            inventoryBox.AddItem(0, 1);
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
        cursorDirection = moveDirection;

        if (MapManager.Instance.map.TryGetValue(GameManager.Instance.GetChunkPosition(transform.position), out Chunk chunk))
        {
            Vector2Int pos = GameManager.Instance.GetTilePosition(transform.position);

            //�R���C�_�[�������Ă���Ȃ�
            if (GameManager.Instance.setting.mapItemTiles[chunk.mapitemdata[pos.x, pos.y]].hasCollider)
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
        // 破壊
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.L))
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

        // 設置
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.K)) 
        {
            if (inventoryBox.PeekItem(handIndex, out int itemid, out int itemquantity))
            {
                var tilepos = (Vector2)transform.position + cursorDirection;

                if (MapManager.Instance.DestroyTile(itemid, tilepos))
                {
                    inventoryBox.GetItem(handIndex, out itemid);
                }
            }
        }
    }
}
