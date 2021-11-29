using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float velocityMove = 3.0f; // equivale ao momento (impulso) a ser dado ao player
    Vector2 Movement = new Vector2(); // detectar movimento pelo teclado
    Rigidbody2D rb2D; // guarda a componente CorpoRigido do Player

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // recebe o componente RigidBody do Player
    }

    // Update is called once per frame
    void Update()
    {
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");
        Movement.Normalize();
        rb2D.velocity = Movement * velocityMove;
    }
}
