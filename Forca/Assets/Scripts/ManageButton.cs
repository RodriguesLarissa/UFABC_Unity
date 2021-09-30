using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        PlayerPrefs.SetInt("score", 0); // Score inicializa com zero
    }

    public void StartMundoGame(){
        SceneManager.LoadScene("Lab1"); // Ao clicar no botão 'Play', irá para a cena do jogo
    }

    //Exibe a tela de créditos e finaliza o jogo
    public void FinalizarJogo(){
        SceneManager.LoadScene("Creditos"); // Ao clicar em 'Exit', encerra o jogo
        Application.Quit(); //Finalizar aplicação
    }
}
