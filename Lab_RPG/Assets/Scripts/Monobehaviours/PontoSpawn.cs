using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Define a posição a quantidade de vezes e o tempo para que um personagem apareça na tela do jogo.
///</summary>

public class PontoSpawn : MonoBehaviour
{
    public GameObject prefabParaSpawn;

    public float intervaloRepeticao;
    // Start is called before the first frame update
    void Start()
    {
        if (intervaloRepeticao > 0)
        {
            InvokeRepeating("SpawnO", 0.0f, intervaloRepeticao);
        }
    }

    public GameObject SpawnO()
    {
        if (prefabParaSpawn != null)
        {
            return Instantiate(prefabParaSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
