using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Define a posição a quantidade de vezes e o tempo para que um personagem apareça na tela do jogo.
///</summary>

public class PontoSpawn : MonoBehaviour
{
    public GameObject prefabParaSpawn; //referência ao GameObject que deve ser carregado em determinado ponto
    public float intervaloRepeticao;   // frequência em que uma entidade desse GameObject deve ser carregada

    // Start is called before the first frame update
    void Start()
    {
        if (intervaloRepeticao > 0)
        {
            InvokeRepeating("SpawnO", 0.0f, intervaloRepeticao);
        }
    }

    /*
     *  Método responsável por carregar o GameObject em questão no ponto para spawn (adicionado manualmente à cena)
     */
    public GameObject SpawnO()
    {
        if (prefabParaSpawn != null)
        {
            return Instantiate(prefabParaSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }

}
