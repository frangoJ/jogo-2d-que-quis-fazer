using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variáveis privadas
    private Rigidbody2D rb;
    private float moveX;

    // Variáveis públicas
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
        if (Input.GetButtonDown("Jump") && (isGrounded || addJumps > 0))
        {
            Jump();
            if (!isGrounded)
            {
                addJumps--;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if(moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(moveX < 0 )
        {
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
            addJumps = 1;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = false;
        }
    }
}
