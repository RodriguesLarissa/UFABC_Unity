using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armas : MonoBehaviour
{
    public GameObject municaoPrefab; //armazena o prefab da munição
    static List<GameObject> municaoPiscina; //piscina de municao
    public int tamanhoPiscina; //tamanho da piscina
    public float velocidadeArma; //velocidade da municao

    public void Awake(){
        if(municaoPiscina == null){
            municaoPiscina = new List<GameObject>();
        }
        for (int i = 0; i < tamanhoPiscina; i++)
        {
            GameObject municaoO = Instantiate(municaoPrefab);
            municaoO.SetActive(false);
            municaoPiscina.Add(municaoO);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            DisparaMunicao();
        }
    }

    public GameObject SpawnMunicao(Vector3 posicao){
        foreach (GameObject municao in municaoPiscina)
        {
            if(municao.activeSelf == false){
                municao.SetActive(true);
                municao.transform.position = posicao;
                return municao;
            }
        }
        return null;
    }

    void DisparaMunicao(){
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject municao = SpawnMunicao(transform.position);
        if(municao != null){
            Arco arcoScript = municao.GetComponent<Arco>();
            float duracaoTrajetoria = 1.0f/ velocidadeArma;
            StartCoroutine(arcoScript.arcoTrajetoria(posicaoMouse, duracaoTrajetoria));           
        }
    }

    private void OnDestroy(){
        municaoPiscina = null;
    }
}
