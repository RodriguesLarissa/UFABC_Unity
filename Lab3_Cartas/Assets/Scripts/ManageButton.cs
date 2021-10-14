using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButton : MonoBehaviour
{
    public ManageCartas manageCartas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameEasy(){
        GameObject.Find("gameManager").GetComponent<ManageCartas>().setDificuldade(1);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo
    }

    public void gameMedium(){
        GameObject.Find("gameManager").GetComponent<ManageCartas>().setDificuldade(3);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo  
    }

    public void gameHard(){
        manageCartas.setDificuldade(4);
        //GameObject.Find("gameManager").GetComponent<ManageCartas>().setDificuldade(4);
        SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo
    }
}
