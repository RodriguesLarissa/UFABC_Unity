using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{   
    public GameObject carta; // A carta a ser descartada
    private bool primeiraCartaSelecionada, segundaCartaSelecionada, terceiraCartaSelecionada, quartaCartaSelecionada; // indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2, carta3, carta4; //gameobjects da 1° e 2° carta selecionada
    private string linhaCarta1, linhaCarta2, linhaCarta3, linhaCarta4; //linha da carta selecionada

    bool timerPausado, timerAcionado; //indicador de pausa no Timer ou Start Timer
    float timer; // variavel de tempo

    int numTentativas = 0; // número de tentativas na rodada
    int numAcertos = 0; // número de match de pares acertados
    AudioSource somOK; // som de acerto
    int ultimoJogo = 0; // numero de tentativas do ultimo jogo
    int recordHard = 0; // record do jogador do jogo dificil
    int recordEasy = 0; // record do jogador do jogo facil

    string dificuldade = ""; //dificuldade do jogo
    
    // Start is called before the first frame update
    public void Start()
    {
        MostraCartas(PlayerPrefs.GetInt("Dificuldade"));
        UpdateTentativas();   
        somOK = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas",0);
        recordHard = PlayerPrefs.GetInt("RecordeHard",0);
        recordEasy = PlayerPrefs.GetInt("RecordEasy",0);
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Jogo Anterior = " + ultimoJogo;
        switch (PlayerPrefs.GetInt("Dificuldade"))
        {
            case 4:
                GameObject.Find("recorde").GetComponent<Text>().text = "Recorde Hard = " + recordHard;
                break;
            default:
                GameObject.Find("recorde").GetComponent<Text>().text = "Recorde = " + recordEasy;
                break;
        }      
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
                if(dificuldade.Equals("hard")){ //modo dificil
                    if(carta1.tag == carta2.tag && carta3.tag == carta4.tag && carta2.tag == carta3.tag){
                        Destroy(carta1);
                        Destroy(carta2);
                        Destroy(carta3);
                        Destroy(carta4);
                        numAcertos++;
                        somOK.Play();
                        if(numAcertos == 13){
                            if(numTentativas < recordHard || recordHard == 0){
                                PlayerPrefs.SetInt("Jogadas", numTentativas);
                                PlayerPrefs.SetInt("RecordHard", numTentativas);
                                SceneManager.LoadScene("Recorde");             
                            }
                            else {
                                PlayerPrefs.SetInt("Jogadas", numTentativas);
                                SceneManager.LoadScene("TelaInicial");  
                            }
                        }
                    } 
                    else {
                        carta1.GetComponent<Tile>().EscondeCarta();
                        carta2.GetComponent<Tile>().EscondeCarta();  
                        carta3.GetComponent<Tile>().EscondeCarta();  
                        carta4.GetComponent<Tile>().EscondeCarta();  
                    }           
                } else { //modo easy
                    if(carta1.tag == carta2.tag){
                        Destroy(carta1);
                        Destroy(carta2);
                        numAcertos++;
                        somOK.Play();
                        if(numAcertos == 13){
                            if(numTentativas < recordEasy || recordEasy == 0){
                                PlayerPrefs.SetInt("Jogadas", numTentativas);
                                PlayerPrefs.SetInt("RecordEasy", numTentativas);
                                SceneManager.LoadScene("Recorde");             
                            }
                            else {
                                PlayerPrefs.SetInt("Jogadas", numTentativas);
                                SceneManager.LoadScene("TelaInicial");  
                            }
                        }
                    }
                    else {
                        carta1.GetComponent<Tile>().EscondeCarta();
                        carta2.GetComponent<Tile>().EscondeCarta();             
                    }         
                }  
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                terceiraCartaSelecionada = false;
                quartaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                carta3 = null;
                carta4 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                linhaCarta3 = "";
                linhaCarta4 = "";
                timer = 0;
            }
        }
    }

    /*
        O método MostraCartas() faz um loop para adicionar 13 cartas ao jogo.
        Um switch case defini o modo que o jogo será exibido com sua dificuldade
    */
    void MostraCartas(int opcaoCartas){
        int[] arrayEmbaralhado = criaArrayEmbaralhado();
        int[] arrayEmbaralhado2 = criaArrayEmbaralhado();
        switch (opcaoCartas)
        {
            case 1: //modo de jogo com naipes vermelhos - easy
                for(int i=0; i<13; i++){
                    AddUmaCarta(0, i, arrayEmbaralhado[i], "Red", "clubs");
                    AddUmaCarta(1, i, arrayEmbaralhado2[i], "Red", "spades");
                    dificuldade = "easy";
                }
                break;
            case 2: //modo de jogo com naipes pretos - easy
                for(int i=0; i<13; i++){
                    AddUmaCarta(0, i, arrayEmbaralhado[i], "Red", "diamonds");
                    AddUmaCarta(1, i, arrayEmbaralhado2[i], "Red", "hearts");
                    dificuldade = "easy";
                }
                break;
            case 3: //modo de jogo com baralhos diferentes - easy (medium)
                for(int i=0; i<13; i++){
                    AddUmaCarta(0, i, arrayEmbaralhado[i], "Red", "clubs");
                    AddUmaCarta(1, i, arrayEmbaralhado2[i], "Blue", "clubs");
                    dificuldade = "easy";
                }
                break;
            case 4: //modo de jogo baralho unico e 4 linhas - hard
                int[] arrayEmbaralhado3 = criaArrayEmbaralhado();
                int[] arrayEmbaralhado4 = criaArrayEmbaralhado();
                           
                for(int i=0; i<13; i++){
                    AddUmaCarta(0, i, arrayEmbaralhado[i], "Red", "clubs");
                    AddUmaCarta(1, i, arrayEmbaralhado2[i], "Red", "spades");
                    AddUmaCarta(2, i, arrayEmbaralhado3[i], "Red", "hearts");
                    AddUmaCarta(3, i, arrayEmbaralhado4[i], "Red", "diamonds");
                    dificuldade = "hard";
                }
                break;
        }   
    }

    /*
        O método AddUmaCarta() define a posição da carta através do Vector3 novaPosicao.
        É definido a tag e o nome da variável da carta como o numero do rank dela.
        Através de uma condicional, avaliamos qual é o rank da carta para definir qual será a Sprite recebida pela carta. Por exemplo, caso o rank seja 0, a Sprite selecionada será a que possui o nome ace_of_clubs.
        Por fim, a Sprite é recarregada e chamamos a função setCartaOriginal com a nova Sprite que será colocada no tile.
    */
    private void AddUmaCarta(int linha, int rank, int valor, string corBaralho, string naipe){
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
    
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(numeroCarta + "_of_" + naipe));
        Sprite s2 = (Sprite)(Resources.Load<Sprite>("playCardBack" + corBaralho));
        print("S1: " + s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaOriginal(s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setBaralho(s2);
        

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

    /* O método CartaSeleciona é chamado ao virar um carta no jogo, nele é feita as verificações
    se o conjunto de cartas selecionadas são iguais. Funciona para cada modo de dificuldade.
    */
    public void CartaSeleciona(GameObject carta){
        switch (dificuldade)
        {
            case "easy": //modo de jogo com 2 linhas
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
                break;
            case "hard": //modo de jogo com 4 linhas
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
                }
                else if (primeiraCartaSelecionada && segundaCartaSelecionada && !terceiraCartaSelecionada){
                    string linha = carta.name.Substring(0, 1);
                    linhaCarta3 = linha;
                    terceiraCartaSelecionada = true;
                    carta3 = carta;
                    carta3.GetComponent<Tile>().RevelaCarta();                  
                }
                else if (primeiraCartaSelecionada && segundaCartaSelecionada && terceiraCartaSelecionada && !quartaCartaSelecionada){
                    string linha = carta.name.Substring(0, 1);
                    linhaCarta4 = linha;
                    quartaCartaSelecionada = true;
                    carta4 = carta;
                    carta4.GetComponent<Tile>().RevelaCarta(); 
                    VerificaCartas();                 
                }
                break;             
        }
    }

    /* O método VerificaCartas é um intermediário e serve para diparar o timer e contar o número de
    tentativas da rodada atual, junto com os métodos subsequentes.
    */
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
