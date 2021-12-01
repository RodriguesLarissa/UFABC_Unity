using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaldeManage : MonoBehaviour
{
    public float speed = 15f;
    private float minBarreira, maxBarreira;
    private Rigidbody2D rigidbody;  
    private Text scoreBox;
    private int score = 0;

    public int pontuacaoVitoria = 25;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        scoreBox = GameObject.Find("Score").GetComponent<Text>();
        scoreBox.text = "Score: 0";
    
    }

    void OnTriggerEnter2D(Collider2D alvo) {
        if(alvo.tag == "Machado"){
            transform.position = new Vector2(0,100);
            alvo.gameObject.SetActive(false);
            StartCoroutine(Restart());
        }

        if(alvo.tag == "Fruta"){
            alvo.gameObject.SetActive(false);
            score++;
            scoreBox.text = "Score: " + score.ToString();

            if(score == pontuacaoVitoria){
                //Chamar aqui a cena de vitoria ou sair 
                //para o mundo do jogo
                StartCoroutine(Restart());
            }
        }
    }

    IEnumerator Restart(){
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start(){
        Vector3 coordenadas = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minBarreira = -coordenadas.x + 1f;
        maxBarreira = coordenadas.x - 1f;
    }
    void FixedUpdate()
    {
        Vector2 velocidade = rigidbody.velocity;
        velocidade.x = Input.GetAxis("Horizontal")*speed;
        rigidbody.velocity = velocidade;
    }

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
