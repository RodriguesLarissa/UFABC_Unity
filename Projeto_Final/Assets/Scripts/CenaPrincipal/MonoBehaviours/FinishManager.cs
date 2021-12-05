using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe de verificacao da condicao de vitoria (saida da dungeon) do player
/// </summary>
public class FinishManager : MonoBehaviour
{
    /*
     * Colisao com o objeto de saida
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            string[] games = { "FruitMiniGame", "JogoDaMemoria", "PulaPlataforma", "Snake" };
            foreach (var game in games)
            {
                if (PlayerPrefs.GetInt(game) == 0)
                {
                    print("Ainda faltam alguns jogos");
                    return;
                }
            }

            SceneManager.LoadScene("Vitoria");
        }
    }
}