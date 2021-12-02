using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Coleta os objetos que não forem pegos pelo player e destroi (inativa por questões de performance)
public class Coletor : MonoBehaviour
{
    //Inativa os objetos que passarem do player e cairem no coletor
    void OnTriggerEnter2D(Collider2D alvo) {
        if(alvo.tag == "Machado" || alvo.tag == "Fruta"){
            //Destroy(alvo.gameObject);
            alvo.gameObject.SetActive(false);
        }
    }
}
