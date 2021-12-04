using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            string[] games = {"FruitMiniGame", "JogoDaMemoria", "PulaPlataforma", "Snake"};
            foreach (var game in games)
            {
                if (PlayerPrefs.GetInt(game) == 0)
                {
                    print("Ainda faltam alguns jogos");
                    return;
                }
            }

            SceneManager.LoadScene("Vitória");
        }
    }
}
