using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Vari�veis privadas
    private Rigidbody2D rb;
    private float moveX;
    private Animator anim;

    // Vari�veis p�blicas
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    void FixedUpdate()
    {
        Move();
        Attack();
    }

    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX > 0) // Lado direito
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("isRun", true);
        }
        else if (moveX < 0) // Lado esquerdo
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("isJump", true);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("attack", -1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
            addJumps = 1;
            anim.SetBool("isJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            isGrounded = false;
        }
    }

    // Fun��o chamada pelo Animation Event
    public void SkinChange()
    {
        Debug.Log("Animation Event SkinChange chamado!");
        // Aqui voc� pode adicionar qualquer l�gica necess�ria, como trocar a textura do jogador
    }
}
