using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        rb2d.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }
}
