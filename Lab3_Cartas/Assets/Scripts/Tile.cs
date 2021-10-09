using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false;  // indicador da carta virada ou não
    public Sprite originalCarta;        // Sprite da carta desejada
    public Sprite backCarta;            // Sprite do avesso da carta

    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 
        O método OnMouseDown() faz que a carta seja revelada ou escondida quando o jogador clicar na carta.
    */
    public void OnMouseDown(){
        print("Você pressionou o Tile"); 
        if(tileRevelada){
            EscondeCarta();
        }
        else{
            RevelaCarta();
        }
    }

    /*
        O método EscondeCarta() faz com que a Sprite seja o verso da carta, portanto, no jogo a carta será virada e escondida para o jogador.
    */
    public void EscondeCarta(){
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    /*
        O método RevelaCarta() faz com que a Sprite seja a frente da carta, portanto, no jogo a carta será virada e revelada para o jogador.
    */
    public void RevelaCarta(){
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    /*
        O método setCartaOriginal() altera a carta original.
    */
    public void setCartaOriginal(Sprite novaCarta){
        originalCarta = novaCarta;
    }

}
