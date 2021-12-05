using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para setar os jogos como não feitos (reset dos PlayerPrefs)
/// </summary>
public class RestartManager : MonoBehaviour
{
    /*
     * Define todos os jogos como não feitos
    */
    void Start()
    {
        string[] games = {"FruitMiniGame", "JogoDaMemoria", "PulaPlataforma", "Snake"};

        foreach (var game in games)
        {
            PlayerPrefs.SetInt(game, 0);
        }
    }
}
