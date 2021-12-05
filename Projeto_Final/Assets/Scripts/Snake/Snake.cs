using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider2D))]   // Coloca o componente BoxCollider2D como depend�ncia da classe Snake

/// <summary>
///  Classe respons�vel pelo comportamento do jogador (Snake)
/// </summary>
public class Snake : MonoBehaviour
{

    private List<Transform> _segments = new List<Transform>();  // Segmentos da cobra
    public Transform segmentPrefab;                 // Transform do segmento da cobra

    private Vector2 direction = Vector2.right;     // Direcao do movimento da cobra

    private int initialSize = 3;            // Tamanho inicial da cobra
    private float initialSpeed = 20.0f;     // Velocidade inicial da cobra
    private float speed = 20.0f;            // Velocidade da cobra
    private int score = 0;                  // Pontuação atual
    private int collissions = 0;            // Número de colisões com o rabo ou parede


    private void Start()
    {
        ResetState();       // Chama a funcao para 'setar' o jogo no estado inicial
    }


    /*
     * Funcao chamada quando o objeto e habilitado
    */
    private void OnEnable()
    {
        StartCoroutine(MoveSnake());    // Inicia a corrotina para mover a cobra
    }


    /*
     * Funcao chamada quando o comportamento e desabilitado
    */
    private void OnDisable()
    {
        StopAllCoroutines();            // Encerra todas as corrotinas
    }


    private void Update()
    {
        // Input de comando de movimento da cobra
        // Se estiver se movendo na horizontal (direcao x != 0)
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))           // W || seta para cima: move para cima
                this.direction = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))    // S || seta para baixo: move para baixo
                this.direction = Vector2.down;
        }

        //Se estiver se movendo na vertical (direcao y != 0)
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))              // A || seta para a esquerda: move para a esquerda
                this.direction = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))        // D || seta para a direita: move para a direita
                this.direction = Vector2.right;
        }

    }


    /*
     * Eventos de colisao da cabeca da cobra com outros objetos
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Comida")
        {
            Grow();
            CheckVictory();
        }          // Colisao da cabeca com a comida: cresce

        else if (collision.tag == "Obstaculo" && collissions == 2)  // Colisao da cabeca com algum "Obstaculo" (rabo ou parede): 
        {
            SceneManager.LoadScene("Derrota");
        }
        else if (collision.tag == "Obstaculo")  // Colisão, porém sem o número de colisões (evita que haja interação com o spawn do corpo)
        {
            // print("Colidiu");    // teste de colisão
            collissions++;
        }
    }

    /*
     * IEnumerator responsavel pelo movimento, direcao e controle da velocidade da cobra
    */
    private IEnumerator MoveSnake()
    {
        while (this.enabled)
        {
            // Define a posicao de cada segmento para ser o mesmo que o seguinte.
            // Deve ser implementado em ordem reversa, ou os segmentos se empilham.
            for (int i = _segments.Count - 1; i > 0; i--)
                _segments[i].position = _segments[i - 1].position;


            // Define a proxima posicao da cobra, segundo a variavel direction
            float x = Mathf.Round(this.transform.position.x) + this.direction.x;
            float y = Mathf.Round(this.transform.position.y) + this.direction.y;
            this.transform.position = new Vector2(x, y);


            // Faz a rotacao do sprite da cobra
            this.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(direction) - 90);


            // Altera a velocidade da cobra, 
            //     manipulando a velocidade de atualizacao da sua corrotina de movimento.
            //     � inversamente proporcional a variavel 'speed', ou seja:
            //     quanto maior a 'speed', menor o tempo para cada atualizacao (maior velocidade de movimento).
            yield return new WaitForSeconds(1.0f / this.speed);
        }
    }


    /*
     * Funcao que recebe um Vector2 e transforma em float. Utilizada para a rotacao do sprite da cobra.
    */
    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
    }


    /*
     * Funcao responsavel pelo aumento da velocidade da cobra. O aumento varia conforme o tamanho de segmentos. 
    */
    private void RaiseSpeed()
    {
        if (_segments.Count < 8)
            speed += 1.0f;
        else if (_segments.Count >= 8)
            speed += 1.5f;
    }


    /*
     * Funcao responsavel pelo crescimento da cobra ao coletar as comidas.
    */
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);            // Instancia um novo pedaco (segmento)
        segment.position = _segments[_segments.Count - 1].position;     // define a posicao do segmento na �ltima

        _segments.Add(segment);     // Aumenta o tamanho da cobra em +1
        RaiseSpeed();               // Aumenta a velocidade da cobra
        AddScore();                 // Aumenta em 1 o score
    }


    /*
     * Funcao para conferir se o score e suficiente para vencer (maior ou igual a 10).
     * Caso nao seja, reseta o score e reinicia o jogo.
     * Caso seja, chama a Funcao de vitoria do minigame.
    */
    private void CheckVictory()
    {
        if (score >= 10)
        {
            Debug.Log("Voce venceu!");
            PlayerPrefs.SetInt("Snake", 1);
            SceneManager.LoadScene("PrincipalScene");
        }
    }


    /*
     * Funcao para redefinir os estados da cobra:
     * Direcao, posicao, segmentos e velocidade.
    */
    private void ResetState()
    {
        speed = initialSpeed;                       // Define a velocidade inicial da cobra
        ResetScore();                               // Reseta o score

        this.direction = Vector2.right;             // Reseta a direcao do movimento da cobra
        this.transform.position = Vector3.zero;     // Posicao zero

        // Varre e destroi os segmentos da cobra, pula o primeiro para nao destruir a cabeca
        for (int i = 1; i < _segments.Count; i++)
            Destroy(_segments[i].gameObject);

        _segments.Clear();                  // Esvazia a lista de segmentos (apos destruir, eles continuam referenciados)
        _segments.Add(this.transform);      // Adiciona a cabeca de volta

        // Cria a quantidade de segmentos iniciais definidos (padrao: 3)
        for (int i = 1; i < initialSize; i++)
            _segments.Add(Instantiate(this.segmentPrefab));


    }


    /*
     * Funcao para incrementar o score em 1 e chama a atualizacao do score na UI
    */
    private void AddScore()
    {
        score += 1;
        SnakeGameManager.Instance.UpdateScore(score);
    }

    /*
     * Funcao para resetar o score do jogador
    */
    private void ResetScore()
    {
        score = 0;
        SnakeGameManager.Instance.UpdateScore(score);
    }
}