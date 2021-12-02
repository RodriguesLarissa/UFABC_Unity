using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerPulo : MonoBehaviour
{
    public float sentidoMovimentacao = 3.0f; // equivale ao momento (impulso) a ser dado ao player
    Vector2 Movement = new Vector2(); // detectar movimento pelo teclado
    Animator animator; // guarda a componente do Controlador de Animação
    string animationState = "animationState"; // guarda o nome do parametro de Animação
    Rigidbody2D rb2D; // guarda a componente CorpoRigido do Player

    public LayerMask verificaChao;  /* verifica se a superficie que o game object feetPos esta
                                     encostando é parte da layer que deve ser considerada chao ou não */
    public float velocidade;        // usado para definir a velocidade do jogador
    public float forcaPulo;         // usado para definir a projeção do player no ar
    private bool estaNoChao;        // Verifica se o player esta no chao para que possa pular, caso contrario não permite essa ação
    public Transform posicaoPes;    // responsável pelo gameObject feetPos que ficará nos pés do player
    public float checaRaio;         // define o tamanho raio do gameObject dos pés do player que checa se está no chao

    /*
        Enumera o número da condição de cada movimento
    */
    enum EstadosCaractere
    {
        andaLeste = 1,
        andaOeste = 2,
        idle = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // recebe o componente animator
        rb2D = GetComponent<Rigidbody2D>(); // recebe o componente RigidBody do Player
    }

    // Update is called once per frame
    void Update()
    {
        // esta no chao é uma variável booleana que irá retornar true caso o raio esteja sofrendo "invasão" de uma camada que é considerada chao 
        estaNoChao = Physics2D.OverlapCircle(posicaoPes.position, checaRaio, verificaChao); 

        // se o player estiver no chao e a tecla espaço for pressionada, arremessa o jogador no ar com a impulsão definida por forcaPulo
        if (estaNoChao == true && Input.GetKeyDown(KeyCode.Space)) 
        {
            rb2D.velocity = Vector2.up * forcaPulo;
        }
        StateUpdate();
    }

    private void FixedUpdate()
    {
        MoveCaractere();
    }

    /*  
        Função reponsável pelo movimento horizontal do player.
    */
    private void MoveCaractere()
    {
        sentidoMovimentacao = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(sentidoMovimentacao * velocidade, rb2D.velocity.y);
    }

    /*
        Função que atualiza o estado do player e o atualiza a sprite conforme o movimento.
    */
    private void StateUpdate()
    {
        if (sentidoMovimentacao > 0)
        {
            animator.SetInteger(animationState, (int)EstadosCaractere.andaLeste);
        }
        else if (sentidoMovimentacao < 0)
        {
            animator.SetInteger(animationState, (int)EstadosCaractere.andaOeste);
        }
        else
        {
            animator.SetInteger(animationState, (int)EstadosCaractere.idle);
        }
    }
}