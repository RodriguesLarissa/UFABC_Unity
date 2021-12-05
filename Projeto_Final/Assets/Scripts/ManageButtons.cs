using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe para gerenciamento dos botões utilizados no jogo
/// </summary>
public class ManageButtons : MonoBehaviour
{
    /*
     * Ao clicar no botão, irá para a cena principal do jogo
     */
    public void start(){
        SceneManager.LoadScene("PrincipalScene"); 
    }

    /*
     * Ao clicar no botão, irá para os créditos
     */
    public void Creditos(){
        SceneManager.LoadScene("Creditos");
    }

    /*
     * Ao clicar no botão, irá para a intro
     */
    public void Intro(){
        SceneManager.LoadScene("Intro"); 
    }

    /*
     * Ao clicar no botão, reinicia o jogo (envia para a tela inicial)
     */
    public void PlayAgain()
    {
        string sceneName = PlayerPrefs.GetString("Scene");
        SceneManager.LoadScene(sceneName);
    }
}
