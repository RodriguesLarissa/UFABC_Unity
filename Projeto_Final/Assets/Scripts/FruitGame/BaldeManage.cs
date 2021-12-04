using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaldeManage : MonoBehaviour
{
    public float speed = 15f;                   //Velocidade do balde
    private float minBarreira, maxBarreira;     //Limites maximos da tela
    private Rigidbody2D rigidBody;              //Rigidbody2D do balde
    private Text scoreBox;                      //Marcador de pontos
    private int score = 0;                      //Pontuação

    public int pontuacaoVitoria = 25;           //Quantidade maxima de pontos para ganhar

    //Desperta as configurações do balde (player) no inicio do minigame
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        scoreBox = GameObject.Find("Score").GetComponent<Text>();
        scoreBox.text = "Score: 0";
    }

    //Detecta colissao entre fruta/machado e o balde e analisa se o objeto foi coletado
    void OnTriggerEnter2D(Collider2D alvo) {
        if(alvo.tag == "Machado"){
            transform.position = new Vector2(0,100);
            alvo.gameObject.SetActive(false);
            SceneManager.LoadScene("Derrota");
        }

        if(alvo.tag == "Fruta"){
            alvo.gameObject.SetActive(false);
            score++;
            scoreBox.text = "Score: " + score.ToString();

            if(score == pontuacaoVitoria){
                //Chamar aqui a cena de vitoria ou sair 
                //para o mundo do jogo
                PlayerPrefs.SetInt("FruitMiniGame", 1);
                SceneManager.LoadScene("PrincipalScene");
            }
        }
    }

    //Reinicia o minigame em caso de perca
    IEnumerator Restart(){
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Inicia o minigame
    void Start(){
        Vector3 coordenadas = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minBarreira = -coordenadas.x + 1f;
        maxBarreira = coordenadas.x - 1f;
    }
    
    //Controla a direção que o balde se movimenta no eixo horizontal usando o teclado
    void FixedUpdate()
    {
        Vector2 velocidade = rigidBody.velocity;
        velocidade.x = Input.GetAxis("Horizontal")*speed;
        rigidBody.velocity = velocidade;
    }

    //Verifica se o balde chegou nos limites do mapa do minigame e o impede de atravessar
    void Update()
    {
        Vector3 temp = transform.position;
        if(temp.x > maxBarreira)
            temp.x = maxBarreira;

        if(temp.x < minBarreira)
            temp.x = minBarreira;

        transform.position = temp;
    }
}
