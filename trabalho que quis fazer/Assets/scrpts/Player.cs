using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variáveis públicas
    public float velocidade = 10f;
    public float focaPulo = 10f;
    public bool noChao = false;
    private Animator anime;

    // Variáveis privadas
    private int pulosRestantes = 1;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        anime = gameObject.GetComponent<Animator>(); // Inicializa o Animator
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = true;
            pulosRestantes = 1; // Pode pular novamente ao tocar o chão
            anime.SetBool("isJump", false); // Desativa a animação de pulo quando estiver no chão
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
        }
    }

    void Update()
    {
        // Verifica se está se movendo para a esquerda ou direita
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = true; // Vira o personagem para a esquerda
            anime.SetBool("isRun", true); // Ativa animação de correr
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = false; // Vira o personagem para a direita
            anime.SetBool("isRun", true); // Ativa animação de correr
        }
        else
        {
            anime.SetBool("isRun", false); // Desativa animação de correr quando não está se movendo
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && pulosRestantes > 0 && noChao)
        {
            _rigidbody2D.AddForce(new Vector2(0, 1) * focaPulo, ForceMode2D.Impulse);
            pulosRestantes--;
            anime.SetBool("isJump", true); // Ativa animação de pulo
            Debug.Log("Jump");
        }
        
        // Verifica se o personagem está no ar e altera a animação de pulo
        if (noChao == false && _rigidbody2D.velocity.y > 0)
        {
            anime.SetBool("isJump", true); // Mantém a animação de pulo enquanto sobe
        }
    }
}

