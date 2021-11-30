using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Classe responsável por organizar todas as mecânicas necessárias para que o minigame aconteça
/// </summary>
public class JogoDaMemoriaManager : MonoBehaviour
{
    public GameObject prefabCartaMedieval;                          //prefab utilizado para instanciar as cartas
    public GameObject player;

    GameObject cartaSelecionada1 = null, cartaSelecionada2 = null;  //par de cartas a ser comparado
    public List<GameObject> cartas;                                 //lista com todas as cartas presentes
    GameObject [] pontosSpawnCartas;                         //vetor com todos os posicionamentos possíveis para as cartas

    // Start is called before the first frame update
    void Start()
    {
        pontosSpawnCartas = GameObject.FindGameObjectsWithTag("PontoSpawnCarta");
        CarregarCartas();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Método responsável por carregas as cartas utilizadas no jogo e organizá-las na tela
     */
    void CarregarCartas()
    {
        for(int i = 0; i<2; i++)
        {
            string[] cartasMedievaisScriptable = AssetDatabase.FindAssets("t:CartaMedieval");
            
            foreach (string cm in cartasMedievaisScriptable)
            {
                string path = AssetDatabase.GUIDToAssetPath(cm).Split('.')[0].Remove(0, 17);    //isola o path corretamente para que a função Resources.Load funcione
                                                                                                //print(path);
                GameObject cartaMedieval = Instantiate(prefabCartaMedieval);
                cartaMedieval.GetComponent<CartaMedievalO>().cartaMedieval = Resources.Load<CartaMedieval>(path);
                cartaMedieval.name = "Carta_" + cartaMedieval.GetComponent<CartaMedievalO>().cartaMedieval.tag + "_" + i;
                cartaMedieval.transform.SetParent(GameObject.Find("cartas").transform);
                //cartaMedieval.GetComponent<SpriteRenderer>().sprite = cartaMedieval.GetComponent<CartaMedievalO>().cartaMedieval.frente;
                cartas.Add(cartaMedieval);
            }

            EmbaralharCartas();

            int j = 0;
            foreach(GameObject carta in cartas)
            {
                carta.transform.position = new Vector3(pontosSpawnCartas[j].transform.position.x, pontosSpawnCartas[j].transform.position.y, 0);
                pontosSpawnCartas[j].GetComponent<pontoDeSpawnCarta>().cartaAtual = carta.name;
                pontosSpawnCartas[j].GetComponent<pontoDeSpawnCarta>().isOccupied = true;
                j++;
            }

        }

    }

    /*
     * Método resposável por randomizar a posição em que as cartas seram apresentadas
     */
    void EmbaralharCartas()
    {
        for (int i = 0; i < cartas.Count; i++)
        {
            int index = Random.Range(0, cartas.Count);

            GameObject aux = cartas[i];
            cartas[i] = cartas[index];
            cartas[index] = aux;

        }
    }

    /*
     * Método responsável pela seleção de um par de cartas para comparação
     */
    public void SelecionarCarta(GameObject carta)
    {
        float distance = Vector2.Distance(player.transform.position, carta.transform.position);

        if(distance <= 2)
        {
            if (cartaSelecionada1 == null)
            {
                cartaSelecionada1 = carta;
                cartaSelecionada1.GetComponent<CartaMedievalO>().MostrarCarta();
            }
            else
            {
                if (cartaSelecionada2 == null)
                {
                    cartaSelecionada2 = carta;
                    cartaSelecionada2.GetComponent<CartaMedievalO>().MostrarCarta();
                    StartCoroutine(VerificarCartas());
                }
            }
        }

    }

    /*
     *IEnumerator responsável por mostrar as cartas selecionadas pelo player por 1 segundo e em seguida verificar se houve um acerto ou um erro,
     *dando um tratamento adequado para os dois casos
     */
    IEnumerator VerificarCartas()
    {
        yield return new WaitForSeconds(1.0f);
        
        if(cartaSelecionada1.GetComponent<CartaMedievalO>().cartaMedieval.tag == cartaSelecionada2.GetComponent<CartaMedievalO>().cartaMedieval.tag)
        {
            cartas.Remove(cartaSelecionada1);
            foreach(GameObject pontoSC in pontosSpawnCartas)
            {
                if(pontoSC.GetComponent<pontoDeSpawnCarta>().cartaAtual == cartaSelecionada1.name)
                {
                    pontoSC.GetComponent<pontoDeSpawnCarta>().cartaAtual = null;
                    pontoSC.GetComponent<pontoDeSpawnCarta>().isOccupied = false;
                    break;
                }
            }
            cartaSelecionada1.SetActive(false);
            cartaSelecionada1 = null;

            cartas.Remove(cartaSelecionada2);
            foreach (GameObject pontoSC in pontosSpawnCartas)
            {
                if (pontoSC.GetComponent<pontoDeSpawnCarta>().cartaAtual == cartaSelecionada2.name)
                {
                    pontoSC.GetComponent<pontoDeSpawnCarta>().cartaAtual = null;
                    pontoSC.GetComponent<pontoDeSpawnCarta>().isOccupied = false;
                    break;
                }
            }
            cartaSelecionada2.SetActive(false);
            cartaSelecionada2 = null;
        }

        else
        {
            cartaSelecionada1.GetComponent<CartaMedievalO>().EsconderCarta();
            cartaSelecionada1 = null;
            cartaSelecionada2.GetComponent<CartaMedievalO>().EsconderCarta();
            cartaSelecionada2 = null;

        }

        //print(cartas.Count);
        if(cartas.Count == 0)
        {
            print("VITÓRIA!");
            //O PLAYER VENCE O JOGO: CHAMAR FUNÇÃO DE RETORNAR A (CENA) SALA INICIAL
        }
    }

}
