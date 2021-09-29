using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        PlayerPrefs.SetInt("score", 0);
    }

    public void StartMundoGame(){
        SceneManager.LoadScene("Lab1");
    }

    //Exibe a tela de créditos e finaliza o jogo
    public void FinalizarJogo(){
        SceneManager.LoadScene("Creditos");
        Application.Quit(); //Finalizar aplicação
    }
}
