using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Perambular : MonoBehaviour
{
    public float velocidadePerseguicao;  //velocidade do Inimigo na área de Spot
    public float velocidadePerambular;  //velocidade do Inimigo passeando
    float velocidadeCorrente;            //velocidade do Inimigo atribuida
    public float intervaloMudancaDirecao;   //tempo para alterar direção
    public bool perseguePlayer;         //indicador de perserguidor ou não
    Coroutine moverCoroutine;
    Rigidbody2D rb2D;           //armazena o componente rigidbody2D
    Animator animator;          //armazena o componente Animator
    Transform alvoTransform = null;      //armazena o componente Transform do Alvo

    Vector3 posicaoFinal;
    float anguloAtual = 0; //angulo atribuido

    CircleCollider2D circleCollider; //armazena o componente de Spot
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocidadeCorrente = velocidadePerambular;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RotinaPerambular());
        circleCollider = GetComponent<CircleCollider2D>();    
        GetComponent<CircleCollider2D>().enabled = false;  // desativa o circle collider collison, o collider passa a ser o box collider
    }

    /*Define o que ocorre caso o colisor seja nulo*/
    private void OnDrawGizmos(){
        if(circleCollider != null){
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    public IEnumerator RotinaPerambular(){
        while(true){
            EscolherNovoPontoFinal();
            if(moverCoroutine != null){
                StopCoroutine(moverCoroutine);
            }
            moverCoroutine = StartCoroutine(Mover(rb2D,velocidadeCorrente));
            yield return new WaitForSeconds(intervaloMudancaDirecao);
        }
    }

    //Escolhe um novo ponto aleatorio para redirecionar o inimigo
    void EscolherNovoPontoFinal(){
        anguloAtual += Random.Range(0,360);
        anguloAtual = Mathf.Repeat(anguloAtual, 360);
        posicaoFinal += Vector3ParaAngulo(anguloAtual);
    }

    //Calcula novo angulo
    Vector3 Vector3ParaAngulo(float anguloEntradaGraus){
        float anguloEntradaRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0);
    }

    //Metodo responsavel pelo movimento do inimigo e ocasional pedido de redirecionamento
    public IEnumerator Mover(Rigidbody2D rBParaMover, float velocidade){
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
        while (distanciaFaltante > float.Epsilon)
        {
             if(alvoTransform != null)
             {
                 posicaoFinal = alvoTransform.position;
             }
             if (rBParaMover != null)
             {
                 animator.SetBool("Caminhando", true);
                 Vector3 novaPosicao = Vector3.MoveTowards(rBParaMover.position, posicaoFinal, velocidade * Time.deltaTime);
                 rb2D.MovePosition(novaPosicao);
                 distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
             }

             yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Caminhando", false);
    }

    //Define o comportamento caso o Player entre no range da "visão" do inimigo
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player") && perseguePlayer){
            velocidadeCorrente = velocidadePerseguicao;
            alvoTransform = collision.gameObject.transform;
            if(moverCoroutine != null){
                StopCoroutine(moverCoroutine);
            }
            moverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
        }
    }

    //Define o comportamento caso o Player saia do range da "visão" do inimigo
    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            animator.SetBool("Caminhando", false);
            velocidadeCorrente = velocidadePerambular;
            if(moverCoroutine != null){
                StopCoroutine(moverCoroutine);
            }
            alvoTransform = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red);
    }
}
