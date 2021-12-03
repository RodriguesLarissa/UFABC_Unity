using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe respons�vel pelo gerenciamento e eventos do objeto a ser coletado (comida)
/// </summary>
public class Comida : MonoBehaviour
{
    public BoxCollider2D gridArea;      // �rea do Grid que ser� usada para 'spawn' da comida

    private void Start()
    {
        RandomizePosition();    //
    }

    // Evento de colis�o da cabe�a da cobra com a comida
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snake")
            RandomizePosition();
    }

    // Converter coment�rio em Summary:
    // aleatoriza a posi��o de 'spawn' da comida no espa�o delimitado pela gridArea.
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;   // Define os limites

        float x = Random.Range(bounds.min.x, bounds.max.x);     // Gera um valor para posi��o aleat�ria em X
        float y = Random.Range(bounds.min.y, bounds.max.y);     // Gera um valor para posi��o aleat�ria em Y

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);    // Posi��o final em que ser� criada a comida
    }
}
