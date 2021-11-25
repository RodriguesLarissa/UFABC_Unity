using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Armas : MonoBehaviour
{
    public GameObject municaoPrefab; //armazena o prefab da munição
    static List<GameObject> municaoPiscina; //piscina de municao
    public int tamanhoPiscina; //tamanho da piscina
    public float velocidadeArma; //velocidade da municao

    bool atirando;
    [HideInInspector]
    public Animator animator;

    Camera cameraLocal;

    /*O Slope serve para indicar qual o quadrante do mouse, considerando cada slope temos quatro combinações que correspondem aos quatro pontos cardeais, de maneira ilustrativa seriam: (slopePositivo, slopeNegativo)
    (!slopePositivo, slopeNegativo), (slopePositivo, !slopeNegativo) e (!slopePositivo, !slopeNegativo)*/
    float slopePositivo;
    float slopeNegativo;

    /*Declara os quadrantes para indicar futuramente qual animação usar baseado na posição do mouse em cada um*/
    enum Quadrante
    {
        Leste,
        Sul,
        Oeste,
        Norte
    }

    // Start is called before the first frame update, inicializando o que irá indicar em qual quadrante se encontra o cursor do mouse*/
    private void Start()
    {
        animator = GetComponent<Animator>();
        atirando = false;
        cameraLocal = Camera.main;
        Vector2 abaixoEsquerda = cameraLocal.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 acimaDireita = cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 acimaEsquerda = cameraLocal.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 abaixoDireita = cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        slopePositivo = PegaSlope(abaixoEsquerda, acimaDireita);
        slopeNegativo = PegaSlope(acimaEsquerda, abaixoDireita);
    }

    /* Checa se o cursor está acima do slope positivo e retorna*/
    bool AcimaSlopePositivo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopePositivo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopePositivo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    /* Checa se o cursor está acima do slope negativo e retorna*/
    bool AcimaSlopeNegativo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopeNegativo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopeNegativo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    /* Aqui ele finalmente checa em qual ponto cardeal se encontra o cursor baseado nos slopes*/
    Quadrante PegaQuadrante()
    {
        Vector2 posicaoMouse = Input.mousePosition;
        Vector2 posicaoPlayer = transform.position;
        bool acimaSlopePositivo = AcimaSlopePositivo(Input.mousePosition);
        bool acimaSlopeNegativo = AcimaSlopeNegativo(Input.mousePosition);
        if (!acimaSlopePositivo && acimaSlopeNegativo)
        {
            return Quadrante.Leste;
        }
        else if (!acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Sul;
        }
        else if(acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Oeste;
        }
        else
        {
            return Quadrante.Norte;
        }
    }

    /*Indica a animação de atirar baseado no quadrante do cursor*/
    void UpdateEstado()
    {
        if (atirando)
        {
            Vector2 vetorQuadrante;
            Quadrante quadranteEnum = PegaQuadrante();
            switch(quadranteEnum)
            {
                case Quadrante.Leste:
                    vetorQuadrante = new Vector2(1.0f, 0.0f);
                    break;
                case Quadrante.Sul:
                    vetorQuadrante = new Vector2(0.0f, -1.0f);
                    break;
                case Quadrante.Oeste:
                    vetorQuadrante = new Vector2(-1.0f, 0.0f);
                    break;
                case Quadrante.Norte:
                    vetorQuadrante = new Vector2(0.0f, 1.0f);
                    break;
                default:
                    vetorQuadrante = new Vector2(0.0f, 0.0f);
                    break;
            }
            animator.SetBool("Atirando", true);
            animator.SetFloat("AtiraX", vetorQuadrante.x);
            animator.SetFloat("AtiraY", vetorQuadrante.y);
            atirando = false;
        }
        else
        {
            animator.SetBool("Atirando", false);
        }
    }

    /*Instancia a munição*/
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

    // Update is called once per frame, recebe o click do botão e atualizaa posição do cursor*/
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            atirando = true;
            DisparaMunicao();
        }
        UpdateEstado();
    }

    /*Como o nome indica, retorna os valores dos Slopes*/
    float PegaSlope(Vector2 ponto1, Vector2 ponto2)
    {
        return (ponto2.y - ponto1.y) / (ponto2.x - ponto1.x);
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

    /*Coordena a animação da munição, que é atirada em trajetória de arco*/
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
