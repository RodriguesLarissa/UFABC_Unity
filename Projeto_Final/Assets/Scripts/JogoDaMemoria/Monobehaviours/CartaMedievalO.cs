using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que representa um objeto Carta Medieval no jogo
/// </summary>
public class CartaMedievalO : MonoBehaviour
{

    public CartaMedieval cartaMedieval;     //scriptable object que armazena as informa��es de uma carta
    bool isHidden;                          //booleano que indica se a carta est� escondida ou n�o

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * M�todo respons�vel pelos processamentos acontecidos ao clicar no collider de uma carta
     */
    private void OnMouseDown()
    {
        GameObject.Find("jogoDaMemoriaManager").GetComponent<JogoDaMemoriaManager>().SelecionarCarta(gameObject);
    }

    /*
     * M�todo respons�vel por expor a frente de uma carta
     */
    public void MostrarCarta()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<CartaMedievalO>().cartaMedieval.frente;
        isHidden = false;
    }

    /*
     * M�todo respons�vel por esconder a frente de uma carta
     */
    public void EsconderCarta()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<CartaMedievalO>().cartaMedieval.verso;
        isHidden = true;
    }

}
