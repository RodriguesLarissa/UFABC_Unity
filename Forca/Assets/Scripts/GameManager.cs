using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int numTentativas; // armazena as tentativas válidas da rodada
    private int maxNumTentativas; // numero maximo de tentativas para Forca ou Salvação
    int score = 0;
    public GameObject letra; // prefab da letra no Game
    public GameObject centro; // objeto de texto que indica o centro da tela

    private string palavraOculta = ""; // palavra a ser descoberta

    //private string[] palavrasOculta = new string[] {"teste","bola","palavra"}; //array de palavras ocultas

    private int tamanhoPalavraOculta; // tamanho da palavra oculta
    char[] letrasOcultas; // letras da palavra oculta
    bool[] letrasDescobertas; // indicador de quais letras foram descobertas

    // Start is called before the first frame update
    void Start(){
        centro = GameObject.Find("centroDaTela");

        InitGame();
        InitLetras();

        numTentativas = 0;
        maxNumTentativas = 10;
        UpdateNumTentativas();
        UpdateScore();
    }

    // Update is called once per frame
    void Update(){
        CheckTeclado();
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
        //int numeroAleatorio = Random.Range(0, palavrasOculta.Length); // sortea uma palavra aleatoria
        //palavraOculta = palavrasOculta[numeroAleatorio];
        
        palavraOculta = PegaUmaPalavraDoArquivo();
        tamanhoPalavraOculta = palavraOculta.Length; // determina-se o numero de letras da palavra oculta
        palavraOculta = palavraOculta.ToUpper(); // transforma-se a palavra em maiuscula
        letrasOcultas = new char[tamanhoPalavraOculta]; // instancia-se o array char das letras da palavra 
        letrasDescobertas = new bool[tamanhoPalavraOculta]; // instancia-se o array bool dos indicador de letras certas
        letrasOcultas = palavraOculta.ToCharArray(); // copia-se a palavra no array de letras
    }

    void CheckTeclado(){
        if(Input.anyKeyDown){
            char letraTeclado = Input.inputString.ToCharArray()[0];
            int letraTecladoInt = System.Convert.ToInt32(letraTeclado);

            if(letraTecladoInt >= 97 && letraTecladoInt <= 122){

                numTentativas++;
                UpdateNumTentativas();

                if(numTentativas > maxNumTentativas){
                    SceneManager.LoadScene("Lab1_forca");
                }

                for (int i = 0; i < tamanhoPalavraOculta; i++){
                    if(!letrasDescobertas[i]){
                        letraTeclado = System.Char.ToUpper(letraTeclado);
                        if(letrasOcultas[i] == letraTeclado){
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i+1)).GetComponent<Text>().text = letraTeclado.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                            verificaSePalavraDescoberta();
                        }
                    }
                }
            }
        }
    }

    void UpdateNumTentativas(){
        GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxNumTentativas;
    }

    void UpdateScore(){
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score;
    }

    void verificaSePalavraDescoberta(){
        bool condicao = true;
        for (int i = 0; i < tamanhoPalavraOculta; i++)
        {
            condicao = condicao && letrasDescobertas[i];
        }
        if(condicao){
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Lab1_salvo");
        }
    }

    string PegaUmaPalavraDoArquivo(){
        TextAsset t1 = (TextAsset)Resources.Load("palavras1", typeof(TextAsset));
        string s = t1.text;
        string[] palavras = s.Split(' ');
        int palavraAleatoria = Random.Range(0, palavras.Length + 1);
        return (palavras[palavraAleatoria]);
    }
}
