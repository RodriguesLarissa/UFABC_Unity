using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsavel pelo spawn de frutas e machados
public class SpawnerManage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fruits; //Vetor de frutas
    private BoxCollider2D collider; //collider onde as frutas sao instanciadas

    float x1, x2; // bordas do collider

    //Inicia o collider e grava seu tamanho nas variaveis de borda
    void Awake(){
        collider = GetComponent<BoxCollider2D>();
        
        x1 = transform.position.x - collider.bounds.size.x / 2f;
        x2 = transform.position.x + collider.bounds.size.x / 2f;

    }
    
    //Inicia o spawn de frutas após 1s
    void Start()
    {
        StartCoroutine(SpawnFruit(1f));
    }

    //Spawna frutas ou machados em um range aleatorio dentro do collider
    //Entra em recursão e spawna frutas entre intervalo aleatorio de 0,5s e 1s 
    IEnumerator SpawnFruit(float time){
        yield return new WaitForSecondsRealtime(time);

        Vector3 temp = transform.position;
        temp.x = Random.Range(x1, x2);

        Instantiate(fruits[Random.Range(0, fruits.Length)], temp, Quaternion.identity);

        StartCoroutine(SpawnFruit(Random.Range(0.5f,1f)));
    }
}
