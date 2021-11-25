using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável por controlar e atualizar os estados de animação do Player
/// </summary>
public class MovimentaPlayer : MonoBehaviour
{
    public float VelocidadeMovimento = 3.0f; // equivale ao momento (impulso) a ser dado ao player
    Vector2 Movimento = new Vector2();       // detectar movimento pelo teclado

    Animator animator; // guarda o componente do Controlador de Animação
    // string EstadoAnimacao = "EstadoAnimacao"; //guarda o nome do parâmetro de animação  //Desnecessário com a Blend Tree (andar tree)
    Rigidbody2D rb2D;   // guarda o componente CorpoRigido do Player

    /*
    enum EstadosCaractere{
        andaLeste = 1,
        andaOeste = 2,
        andaNorte = 3,
        andaSul = 4,
        idle = 5
    }
    */     //Desnecessário com a Blend tree(andar tree)

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEstado();
        
    }

    private void FixedUpdate(){
        MoveCaractere();
    }

    /*
     * Método responsável por receber o input do jogador e movimentar o Player
     */
    private void MoveCaractere(){
        Movimento.x = Input.GetAxisRaw("Horizontal");
        Movimento.y = Input.GetAxisRaw("Vertical");
        Movimento.Normalize();
        rb2D.velocity = Movimento * VelocidadeMovimento;  
    }

    /*
     * Método responsável por receber atualizar o estado de animação do Player com base na sua movimentação
     */

    void UpdateEstado()
    {
        if(Mathf.Approximately(Movimento.x, 0) && (Mathf.Approximately(Movimento.y, 0)))
        {
            animator.SetBool("Caminhando", false);
        }
        else
        {
            animator.SetBool("Caminhando", true);
        }
        animator.SetFloat("dirX", Movimento.x);
        animator.SetFloat("dirY", Movimento.y);
    }

    /*       //Desnecessário
    private void UpdateEstado(){
        /*       //Desnecessário
        if(Movimento.x > 0)
            animator.SetInteger(EstadoAnimacao, (int)EstadosCaractere.andaLeste);
        else if (Movimento.x < 0)
            animator.SetInteger(EstadoAnimacao, (int)EstadosCaractere.andaOeste);
        else if (Movimento.y < 0)
            animator.SetInteger(EstadoAnimacao, (int)EstadosCaractere.andaNorte);
        else if (Movimento.y > 0)
            animator.SetInteger(EstadoAnimacao, (int)EstadosCaractere.andaSul);
        else
            animator.SetInteger(EstadoAnimacao, (int)EstadosCaractere.idle);
     }
    */
}
