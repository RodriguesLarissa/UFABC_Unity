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

    //Chama o metodo que reseta os valores do personagem para um novo jogo
    private void OnEnable() 
    {
        ResetCharacter();      
    }

    /*
     * M�todo associado ao(s) collider(s) dos inimigos que indica as a��es que devem ocorrer quando esses
     * come�am a interagir(em) com outros GameObjects no jogo
     */ 
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

    /*
     * M�todo associado ao(s) collider(s) dos inimigos que indica as a��es que devem ocorrer quando esses
     * terminam de interagir(em) com outros GameObjects no jogo
     */
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

    //Define o que ocorre quando o player toma dano, no caso ver se ele irá
    //morrer ou se deve aguardar mais um tempo até tomar dano novamente
    public override IEnumerator DanoCaractere(int dano, float intervalo) 
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
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

    /*
     * M�todo que indica como os atributos associados ao INimigos devem ser recarregados
     */
    public override void ResetCharacter()
    {
        healthPoints = initialhealthPoints;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
