using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsável pelo gerenciamento do jogo Pula Plataforma.
/// </summary>
public class MovePlayerPulo : MonoBehaviour
{
    public float sentidoMovimentacao = 3.0f; // equivale ao momento (impulso) a ser dado ao player
    Vector2 Movement = new Vector2(); // detectar movimento pelo teclado
    Animator animator; // guarda a componente do Controlador de Anima��o
    string animationState = "animationState"; // guarda o nome do parametro de Animacao
    Rigidbody2D rb2D; // guarda a componente CorpoRigido do Player

    public LayerMask verificaChao;  /* verifica se a superficie que o game object feetPos esta
                                     encostando a parte da layer que deve ser considerada chao ou nao */
    public float velocidade;        // usado para definir a velocidade do jogador
    public float forcaPulo;         // usado para definir a projecao do player no ar
    private bool estaNoChao;        // Verifica se o player esta no chao para que possa pular, caso contrario nao permite essa acao
    public Transform posicaoPes;    // respons�vel pelo gameObject feetPos que ficara nos pes do player
    public float checaRaio;         // define o tamanho raio do gameObject dos p�s do player que checa se esta no chao

    /*
        Enumera o n�mero da condicao de cada movimento
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

    /*
     * Verifica a colisão com a porta e caso haja termina o jogo
    */
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Obstaculo")
        {
            PlayerPrefs.SetInt("PulaPlataforma", 1);
            SceneManager.LoadScene("PrincipalScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // esta no chao e uma variavel booleana que ira retornar true caso o raio esteja sofrendo "invasao" de uma camada que e considerada chao 
        estaNoChao = Physics2D.OverlapCircle(posicaoPes.position, checaRaio, verificaChao); 

        // se o player estiver no chao e a tecla espaco for pressionada, arremessa o jogador no ar com a impulsao definida por forcaPulo
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
        Funcao reponsavel pelo movimento horizontal do player.
    */
    private void MoveCaractere()
    {
        sentidoMovimentacao = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(sentidoMovimentacao * velocidade, rb2D.velocity.y);
    }

    /*
        Funcao que atualiza o estado do player e o atualiza a sprite conforme o movimento.
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