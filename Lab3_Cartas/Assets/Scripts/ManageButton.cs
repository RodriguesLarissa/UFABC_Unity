using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButton : MonoBehaviour
{    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Inicia o jogo na dificuldade facil
    public void gameEasy(){
        PlayerPrefs.SetInt("Dificuldade", 1);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo
    }

    // Inicia o jogo na dificuldade medio
    public void gameMedium(){
        PlayerPrefs.SetInt("Dificuldade", 3);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo  
    }

    // Inicia o jogo na dificuldade dificil
    public void gameHard(){
        PlayerPrefs.SetInt("Dificuldade", 4);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo
    }

    /* Vai para tela de creditos (encerramento) e limpa as preferencias de usuario para
    começar um jogo novo posteriormente 
    */
    public void Creditos(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Creditos"); // Ao clicar no botão, irá para a cena do jogo
    }
}
