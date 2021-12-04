using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Classe responsavel por controlar o dialogo do NPC. 
///</summary>
public class Dialogue : MonoBehaviour
{
    public Sprite profile; // Imagem do NPC
    public string[] speechText; // Texto que o NPC irá falar
    public string actorName; // Nome do NPC 

    public LayerMask playerLayer; // Layer do Jogador
    public float radius; // Raio da area onde será possivel interagir com o NPC
    public string sceneName; // Nome da cena a ser carregada
    bool onRadius = false; // Indica se o jogador está no raio de interação
    bool inDialogue = false; // Indica se o dialogo já foi iniciado

    private DialogueManager dc; // Controlador de dialogo

    private void Start() 
    {
        dc = FindObjectOfType<DialogueManager>(); // Busca o controlodor de dialogo na cena
    }

    /*
     * Chama a função de interação com o jogador a cada frame fisico.
     */
    private void FixedUpdate() 
    {
        Interact();
    }

    /* 
     * Quando o jogador está no raio de interação e pressiona o espaço a função que mostra o 
     * dialogo é chamada
    */
    private void Update() 
    {
        inDialogue = dc.inDialogue;
        print(inDialogue);
        if (Input.GetKeyDown(KeyCode.Space) && onRadius && !inDialogue)
        {
            dc.Speech(profile, speechText, actorName, sceneName);
            onRadius = false;
            inDialogue = true;
        }
    }

    /* 
     *Função que detecta quando o player está na area de interação do npc
     */
    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        onRadius = hit != null ? true : false;
    }

    /* 
     * Função para mostrar a área de interação do NPC, somente no Unity
     */
    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
