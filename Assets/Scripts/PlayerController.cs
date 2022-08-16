using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator animator;
    public float speed;
    public PlayerInventoryBox inventoryBox = new PlayerInventoryBox();
    public int handIndex;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            inventoryBox.AddItem(handIndex, 1);
        }
    }

    void Update()
    {
        rb2d.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        if (rb2d.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        else if(rb2d.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

        animator.SetBool("isWalk", rb2d.velocity != Vector2.zero);


        if (Input.GetMouseButton(0))
        {
            Vector2Int cursorChunkPosition = GameManager.Instance.GetChunkPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            MapManager.Instance.FillChunk(cursorChunkPosition, 1);
            MapManager.Instance.SetTilemap(cursorChunkPosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (inventoryBox.GetItem(handIndex, out int itemid))
            {
                MapManager.Instance.SetTile(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1, itemid);
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
