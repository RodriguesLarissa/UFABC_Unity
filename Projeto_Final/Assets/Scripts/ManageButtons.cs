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

    public void start(){
        SceneManager.LoadScene("PrincipalScene"); // Ao clicar no botão, irá para a cena do jogo
    }

    public void Creditos(){
        SceneManager.LoadScene("Creditos"); // Ao clicar no botão, irá para os créditos
    }

    public void Intro(){
        SceneManager.LoadScene("Intro"); // Ao clicar no botão, irá para os créditos
    }

}
