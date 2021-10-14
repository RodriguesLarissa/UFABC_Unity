using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageRecord : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator  Start()
    {
        yield return StartCoroutine(EsperaImprime(5.0F));
        SceneManager.LoadScene("TelaInicial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EsperaImprime(float waitTime) {
        yield return new WaitForSeconds(waitTime);
    }
}
