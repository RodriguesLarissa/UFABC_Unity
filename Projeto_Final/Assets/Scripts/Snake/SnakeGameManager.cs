using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsável pelo gerenciamento do placar do jogador (Snake)
/// </summary>
public class SnakeGameManager : MonoBehaviour
{
    private static SnakeGameManager instance = null;    // Cria a instancia do SnakeGameManager

    // Inicia a instância do SnakeGameManager
    public static SnakeGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SnakeGameManager();
            }
            return instance;
        }
    }

    // Caso o jogador aperte 'Esc', fecha a aplicação
    //    private void Update()
    //    {
    //        if (Input.GetKey(KeyCode.Escape))
    //        {
    //#if UNITY_EDITOR
    //            UnityEditor.EditorApplication.isPlaying = false;
    //#else
    //            Application.Quit();
    //#endif
    //        }
    //    }


    // Acrescentar addToScore no placar do jogador
    public void UpdateScore(int score)
    {
        Text scoreText = GameObject.Find("Score").GetComponent<Text>();

        scoreText.text = score.ToString() + " / 10";

        if (score < 7)
            GameObject.Find("Score").GetComponent<Text>().color = Color.red; // Changes the colour of the text
        if (score >= 7 && score < 10)
            GameObject.Find("Score").GetComponent<Text>().color = Color.yellow; // Changes the colour of the text
        if (score >= 10)
            GameObject.Find("Score").GetComponent<Text>().color = Color.green; // Changes the colour of the text
    }
}
