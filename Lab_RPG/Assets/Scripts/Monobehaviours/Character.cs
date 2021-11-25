using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe usada para definir as caracteristicas compartilhadas entre os personagens do jogo, como o player e os inimigos.
/// </summary>

public abstract class Character : MonoBehaviour
{
    public int initialhealthPoints; // Quantidade inicial de pontos de vida
    public int maxHealthPoints; // Quantidade Máxima de pontos de vida

    /*
     * Método responsável por indicar o que deve ocorrer a algum personagem do RPG
     * ser determinado como "morto": seu GameObject é destuído
     */ 
    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

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
