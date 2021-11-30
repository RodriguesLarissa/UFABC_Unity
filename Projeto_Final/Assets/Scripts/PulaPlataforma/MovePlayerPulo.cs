using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerPulo : MonoBehaviour
{
    public float velocityMove = 3.0f; // equivale ao momento (impulso) a ser dado ao player
    Vector2 Movement = new Vector2(); // detectar movimento pelo teclado
    Animator animator; // guarda a componente do Controlador de Animação
    string animationState = "animationState"; // guarda o nome do parametro de Animação
    Rigidbody2D rb2D; // guarda a componente CorpoRigido do Player

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
        StateUpdate();
    }

    private void FixedUpdate()
    {
        MoveCaractere();
    }

    /*  
        Função que movimenta o player.
    */
    private void MoveCaractere()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.Normalize();
        rb2D.velocity = Movement * velocityMove;
    }

    /*
        Função que atualiza o estado do player e o atualiza a sprite conforme o movimento.
    */
    private void StateUpdate()
    {
        if (Movement.x > 0)
        {
            animator.SetInteger(animationState, (int)EstadosCaractere.andaLeste);
        }
        else if (Movement.x < 0)
        {
            animator.SetInteger(animationState, (int)EstadosCaractere.andaOeste);
        }
        else
        {
            animator.SetInteger(animationState, (int)EstadosCaractere.idle);
        }
    }
}
