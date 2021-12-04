using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButtons : MonoBehaviour
{
    public void start(){
        SceneManager.LoadScene("PrincipalScene"); // Ao clicar no botão, irá para a cena do jogo
    }

    public void Creditos(){
        SceneManager.LoadScene("Creditos"); // Ao clicar no botão, irá para os créditos
    }

    public void Intro(){
        SceneManager.LoadScene("Intro"); // Ao clicar no botão, irá para os créditos
    }

    public void PlayAgain()
    {
        string sceneName = PlayerPrefs.GetString("Scene");
        SceneManager.LoadScene(sceneName);
    }
}
