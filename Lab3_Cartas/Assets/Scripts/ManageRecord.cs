using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageRecord : MonoBehaviour
{
    // Start is called before the first frame update
    // Start classe alterada para conseguir diparar um timer;
    IEnumerator  Start()
    {
        yield return StartCoroutine(Timer(5.0F));
        SceneManager.LoadScene("TelaInicial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        Classe que dispara um timer, é passado um valor em float representando a quantidade
        segundos que o programa vai esperar para executar a proxima instrução.
    */
    IEnumerator Timer(float waitTime) {
        yield return new WaitForSeconds(waitTime);
    }
}
