using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável pelo gerenciamento e eventos do objeto a ser coletado (comida)
/// </summary>
public class Comida : MonoBehaviour
{
    public BoxCollider2D gridArea;      // Área do Grid que será usada para 'spawn' da comida

    private void Start()
    {
        RandomizePosition();    //
    }

    // Evento de colisão da cabeça da cobra com a comida
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake")
            RandomizePosition();
    }

    // Converter comentário em Summary:
    // aleatoriza a posição de 'spawn' da comida no espaço delimitado pela gridArea.
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;   // Define os limites

        float x = Random.Range(bounds.min.x, bounds.max.x);     // Gera um valor para posição aleatória em X
        float y = Random.Range(bounds.min.y, bounds.max.y);     // Gera um valor para posição aleatória em Y

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);    // Posição final em que será criada a comida
    }
}
