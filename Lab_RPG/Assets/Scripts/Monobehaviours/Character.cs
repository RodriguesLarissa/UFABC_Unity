using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe usada para definir as caracteristicas compartilhadas entre os personagens do jogo, como o player e os inimigos.
/// </summary>

public abstract class Character : MonoBehaviour
{
    public PontosDano healthPoints; // Quantidade atual de pontos de vida
    public int initialhealthPoints; // Quantidade inicial de pontos de vida
    public int maxHealthPoints; // Quantidade Máxima de pontos de vida
}
