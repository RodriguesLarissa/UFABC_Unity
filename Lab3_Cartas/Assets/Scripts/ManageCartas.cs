using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ManageCartas : MonoBehaviour
{   

    public GameObject carta; // A carta a ser descartada

    // Start is called before the first frame update
    void Start()
    {
        MostraCartas();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        O método MostraCartas() faz um loop para adicionar 13 cartas ao jogo.
    */
    void MostraCartas(){
        for(int i=0; i<13; i++){
            AddUmaCarta(i);
        }
    }

    /*
        O método AddUmaCarta() define a posição da carta através do Vector3 novaPosicao.
        É definido a tag e o nome da variável da carta como o numero do rank dela.
        Através de uma condicional, avaliamos qual é o rank da carta para definir qual será a Sprite recebida pela carta. Por exemplo, caso o rank seja 0, a Sprite selecionada será a que possui o nome ace_of_clubs.
        Por fim, a Sprite é recarregada e chamamos a função setCartaOriginal com a nova Sprite que será colocada no tile.
    */
    private void AddUmaCarta(int rank){
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal)/100.0f; 
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + (rank-13/2)*fatorEscalaX, centro.transform.position.y, centro.transform.position.z);    
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        
        c.tag = "" + rank;
        c.name = "" + rank;
        string nomeDaCarta = "";
        string numeroCarta = "";

        if(rank == 0){
            numeroCarta = "ace";
        }
        else if(rank == 10){
            numeroCarta = "jack";
        }
        else if(rank == 11){
            numeroCarta = "queen";
        }
        else if(rank == 12){
            numeroCarta = "king";
        }
        else{
            numeroCarta = "" + (rank+1);
        }

        nomeDaCarta = numeroCarta + "_of_clubs";

        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);
        GameObject.Find("" + rank).GetComponent<Tile>().setCartaOriginal(s1);

    }
}
