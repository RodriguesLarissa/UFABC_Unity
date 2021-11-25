using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageButtons : MonoBehaviour
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
        SceneManager.LoadScene("Lab_RPG_Setup"); // Ao clicar no botão, irá para a cena do jogo
    }

    public void gameHard(){
        //SceneManager.LoadScene("Lab3"); // Ao clicar no botão, irá para a cena do jogo  
    }

    public void Creditos(){
        SceneManager.LoadScene("Cred"); // Ao clicar no botão, irá para os créditos
    }

    public void TelaInicial(){
        SceneManager.LoadScene("Tela_Inicial"); // Ao clicar no botão, irá para os créditos
    }

}
