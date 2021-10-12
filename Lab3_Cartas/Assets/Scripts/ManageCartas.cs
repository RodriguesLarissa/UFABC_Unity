using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{   
    public GameObject carta; // A carta a ser descartada
    private bool primeiraCartaSelecionada, segundaCartaSelecionada; // indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2; //gameobjects da 1° e 2° carta selecionada
    private string linhaCarta1, linhaCarta2; //linha da carta selecionada

    bool timerPausado, timerAcionado; //indicador de pausa no Timer ou Start Timer
    float timer; // variavel de tempo

    int numTentativas = 0; // número de tentativas na rodada
    int numAcertos = 0; // número de match de pares acertados
    AudioSource somOK; // som de acerto
    int ultimoJogo = 0;


    // Start is called before the first frame update
    void Start()
    {
        MostraCartas(); 
        UpdateTentativas();   
        somOK = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas",0);
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Jogo Anterior = " + ultimoJogo;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerAcionado){
            timer += Time.deltaTime;
            print(timer);
            if(timer>1){
                timerPausado = true;
                timerAcionado = false;
                if(carta1.tag == carta2.tag){
                    Destroy(carta1);
                    Destroy(carta2);
                    numAcertos++;
                    somOK.Play();
                    if(numAcertos == 13){
                        PlayerPrefs.SetInt("Jogadas", numTentativas);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
                else {
                    carta1.GetComponent<Tile>().EscondeCarta();
                    carta2.GetComponent<Tile>().EscondeCarta();
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                timer = 0;
            }
        }
    }

    /*
        O método MostraCartas() faz um loop para adicionar 13 cartas ao jogo.
    */
    void MostraCartas(){
        int[] arrayEmbaralhado = criaArrayEmbaralhado();
        int[] arrayEmbaralhado2 = criaArrayEmbaralhado();
        for(int i=0; i<13; i++){
            AddUmaCarta(0, i, arrayEmbaralhado[i]);
            AddUmaCarta(1, i, arrayEmbaralhado2[i]);
        }
    }

    /*
        O método AddUmaCarta() define a posição da carta através do Vector3 novaPosicao.
        É definido a tag e o nome da variável da carta como o numero do rank dela.
        Através de uma condicional, avaliamos qual é o rank da carta para definir qual será a Sprite recebida pela carta. Por exemplo, caso o rank seja 0, a Sprite selecionada será a que possui o nome ace_of_clubs.
        Por fim, a Sprite é recarregada e chamamos a função setCartaOriginal com a nova Sprite que será colocada no tile.
    */
    private void AddUmaCarta(int linha, int rank, int valor){
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal)/110.0f; 
        float fatorEscalaY = (945 * escalaCartaOriginal)/110.0f; 
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + (rank-13/2)*fatorEscalaX, centro.transform.position.y, centro.transform.position.z);    
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + (rank-13/2)*fatorEscalaX, centro.transform.position.y + ((linha -2/2) * fatorEscalaY), centro.transform.position.z);    
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        
        c.tag = "" + (valor);
        c.name = "" + linha + "_" + valor;
        string nomeDaCarta = "";
        string numeroCarta = "";

        if(valor == 0){
            numeroCarta = "ace";
        }
        else if(valor == 10){
            numeroCarta = "jack";
        }
        else if(valor == 11){
            numeroCarta = "queen";
        }
        else if(valor == 12){
            numeroCarta = "king";
        }
        else{
            numeroCarta = "" + (valor+1);
        }

        nomeDaCarta = numeroCarta + "_of_clubs";
        // if (linha == 0)
        //     nomeDaCarta = numeroCarta + "_of_clubs";
        // else 
        //     nomeDaCarta = numeroCarta + "_of_hearts";
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaOriginal(s1);

    }

    /*
        O método criaArrayEmbaralhado() embaralha as cartas e retorna um vetor com a nova ordem.
        Um novo vetor é criado com 13 posições e utilizando a função Random é feito o embaralhamento
        dessas posições usando um for e um varivel temporario. Por fim a função retorna o vetor 
        embaralhado.
    */
    public int[] criaArrayEmbaralhado(){
        int[] novoArray = new int[] {0,1,2,3,4,5,6,7,8,9,10,11,12};
        int temp;
        for (int t = 0; t < 13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t,13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }
        return novoArray;
    }

    public void CartaSeleciona(GameObject carta){
        if(!primeiraCartaSelecionada){
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;
            carta1.GetComponent<Tile>().RevelaCarta();
        }
        else if (primeiraCartaSelecionada && !segundaCartaSelecionada){
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            segundaCartaSelecionada = true;
            carta2 = carta;
            carta2.GetComponent<Tile>().RevelaCarta();
            VerificaCartas();
        }
    }

    public void VerificaCartas(){
        DisparaTimer();
        numTentativas++;
        UpdateTentativas();
    }

    public void DisparaTimer(){
        timerPausado = false;
        timerAcionado = true;
    }

    void UpdateTentativas(){
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
