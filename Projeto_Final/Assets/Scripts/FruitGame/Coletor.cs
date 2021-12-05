using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Coleta os objetos que não forem pegos pelo player e os 'destroi' (inativa, por questões de performance)
/// </summary>
public class Coletor : MonoBehaviour
{
    /*
     * Inativa os objetos que passarem do player e cairem no coletor
    */
    void OnTriggerEnter2D(Collider2D alvo) {
        if(alvo.tag == "Machado" || alvo.tag == "Fruta"){
            //Destroy(alvo.gameObject);
            alvo.gameObject.SetActive(false);
        }
    }
}
