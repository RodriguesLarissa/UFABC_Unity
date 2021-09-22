using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject letra; // prefab da letra no Game
    public GameObject centro; // objeto de texto que indica o centro da tela

    private string palavraOculta = ""; // palavra a ser descoberta

    private string[] palavrasOculta = new string[] {"teste","bola","palavra"}; //array de palavras ocultas
    private int tamanhoPalavraOculta; // tamanho da palavra oculta
    char[] letrasOcultas; // letras da palavra oculta
    bool[] letrasDescobertas; // indicador de quais letras foram descobertas

    // Start is called before the first frame update
    void Start(){
        centro = GameObject.Find("centroDaTela");
        InitGame();
        InitLetras();
    }

    // Update is called once per frame
    void Update(){
        checkTeclado();
    }

    void InitLetras(){
        int numLetras = tamanhoPalavraOculta;
        
        for(int i=0; i<numLetras; i++){
            Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80), centro.transform.position.y, centro.transform.position.z);
            GameObject l = (GameObject) Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i+1);
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    void InitGame(){
        //palavraOculta = "Elefante"; // definição da palavra a ser descoberta

        int numeroAleatorio = Random.Range(0, palavrasOculta.Length); // sortea uma palavra aleatoria
        palavraOculta = palavrasOculta[numeroAleatorio];
        tamanhoPalavraOculta = palavraOculta.Length; // determina-se o numero de letras da palavra oculta
        palavraOculta = palavraOculta.ToUpper(); // transforma-se a palavra em maiuscula
        letrasOcultas = new char[tamanhoPalavraOculta]; // instancia-se o array char das letras da palavra 
        letrasDescobertas = new bool[tamanhoPalavraOculta]; // instancia-se o array bool dos indicador de letras certas
        letrasOcultas = palavraOculta.ToCharArray(); // copia-se a palavra no array de letras
    }

    void checkTeclado(){
        if(Input.anyKeyDown){
            char letraTeclado = Input.inputString.ToCharArray()[0];
            int letraTecladoInt = System.Convert.ToInt32(letraTeclado);

            if(letraTecladoInt >= 97 && letraTecladoInt <= 122){
                for (int i = 0; i < tamanhoPalavraOculta; i++){
                    if(!letrasDescobertas[i]){
                        letraTeclado = System.Char.ToUpper(letraTeclado);
                        if(letrasOcultas[i] == letraTeclado){
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i+1)).GetComponent<Text>().text = letraTeclado.ToString();
                        }
                    }
                }
            }
        }
    }
}
