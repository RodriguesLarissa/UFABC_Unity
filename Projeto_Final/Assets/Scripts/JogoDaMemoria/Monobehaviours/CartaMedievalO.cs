using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que representa um objeto Carta Medieval no jogo
/// </summary>
public class CartaMedievalO : MonoBehaviour
{

    public CartaMedieval cartaMedieval;     //scriptable object que armazena as informações de uma carta
    bool isHidden;                          //booleano que indica se a carta está escondida ou não

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Método responsável pelos processamentos acontecidos ao clicar no collider de uma carta
     */
    private void OnMouseDown()
    {
        GameObject.Find("jogoDaMemoriaManager").GetComponent<JogoDaMemoriaManager>().SelecionarCarta(gameObject);
    }

    /*
     * Método responsável por expor a frente de uma carta
     */
    public void MostrarCarta()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<CartaMedievalO>().cartaMedieval.frente;
        isHidden = false;
    }

    /*
     * Método responsável por esconder a frente de uma carta
     */
    public void EsconderCarta()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<CartaMedievalO>().cartaMedieval.verso;
        isHidden = true;
    }

}
