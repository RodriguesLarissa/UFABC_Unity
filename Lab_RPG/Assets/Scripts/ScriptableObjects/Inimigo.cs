using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Classe que controla o inimigo no jogo
///</summary>


public class Inimigo : Character
{
    float healthPoints; // Quantidade atual de pontos de vida
    public int forcaDano; // quantidade de dano

    Coroutine danoCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        ResetCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (danoCoroutine == null)
            {
                danoCoroutine = StartCoroutine(player.DanoCaractere(forcaDano, 1.0f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        }
    }

    public override IEnumerator DanoCaractere(int dano, float intervalo) 
    {
        while (true)
        {
            healthPoints = healthPoints - dano;
            if (healthPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else
            {
                break;
            }
        }
    }

    public override void ResetCharacter()
    {
        healthPoints = initialhealthPoints;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
