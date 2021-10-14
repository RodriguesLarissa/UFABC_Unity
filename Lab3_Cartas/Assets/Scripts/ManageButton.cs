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

    public void gameEasy(){
        PlayerPrefs.SetInt("Dificuldade", 1);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo
    }

    public void gameMedium(){
        PlayerPrefs.SetInt("Dificuldade", 3);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo  
    }

    public void gameHard(){
        PlayerPrefs.SetInt("Dificuldade", 4);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo
    }

    public void Creditos(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Ending"); // Ao clicar no botão, irá para a cena do jogo
    }
}
