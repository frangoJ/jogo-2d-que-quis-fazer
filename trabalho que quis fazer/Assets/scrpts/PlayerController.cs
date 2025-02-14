using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variaveis privadas
    private Rigidbody2D rb;
    private float moveX;
    
    //variaveis publicas
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        moveX();
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);

        if (isGrounded)
        {
            addJumps = 2;
            if (Input.GetButtonDown("jump"))
            {
                jump();
            }
        }
    }
    else

    {
        if (Input.GetMouseButtonDown("jump") && addJumps > 0)
        {
            addJumps--;
            jump();
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);    
    }

    void jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (Collision.gameObject.tag == "ground")
        {
            isGrounded = false
        }
    }
}
