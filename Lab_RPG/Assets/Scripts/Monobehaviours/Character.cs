using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe usada para definir as caracteristicas compartilhadas entre os personagens do jogo, como o player e os inimigos.
/// </summary>

public abstract class Character : MonoBehaviour 
{
    public int initialhealthPoints; // Quantidade inicial de pontos de vida
    public PontosDano pontosDano; // novo tipo que tem o valor do objeto script
    public int maxHealthPoints; // Quantidade Máxima de pontos de vida
    public float inicioPontosDano; // valor mínimo inicial de "saúde" do Player
    public float MaxPontosDano; // valor máximo permitido de "saúde" do Player

    /*
     * Método responsável por indicar o que deve ocorrer a algum personagem do RPG
     * ser determinado como "morto": seu GameObject é destuído
     */ 
    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    //Define a animação de flickr para indicar que o personagem ou o inimigo tomou dano
    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    /*
     * Método abstrato que será implementado para cada tipo de caracter indicando como seus atributos devem ser reiniciados
     */
    public abstract void ResetCharacter();

    public abstract IEnumerator DanoCaractere(int dano, float intervalo);
}
