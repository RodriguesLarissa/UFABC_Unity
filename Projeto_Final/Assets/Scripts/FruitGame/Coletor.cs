using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D alvo) {
        if(alvo.tag == "Machado" || alvo.tag == "Fruta"){
            //Destroy(alvo.gameObject);
            alvo.gameObject.SetActive(false);
        }
    }
}
