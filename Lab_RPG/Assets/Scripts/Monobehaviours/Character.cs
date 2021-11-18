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

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();

    public abstract IEnumerator DanoCaractere(int dano, float intervalo);
}
