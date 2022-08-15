using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Animator animator;
    public float speed;

    void Start()
    {
        
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
        Debug.Log(animator.GetBool("isWalk"));
    }
}
