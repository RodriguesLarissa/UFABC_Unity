using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : MonoBehaviour
{
    //Calcula a trajet�ria do arco definida pelo ponto onde o player se encontra
    //e o ponto em que a muni��o deve cair
    public IEnumerator arcoTrajetoria(Vector3 destino, float duracao){
        var posicaoInicial = transform.position;
        var percentualCompleto = 0.0f;
        while (percentualCompleto < 1.0f)
        {
             percentualCompleto += Time.deltaTime/ duracao;
             var alturaCorrente = Mathf.Sin(Mathf.PI * percentualCompleto);
             transform.position = Vector3.Lerp(posicaoInicial, destino, percentualCompleto) + Vector3.up*alturaCorrente;
             percentualCompleto += Time.deltaTime / duracao;
             yield return null;
        }
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
