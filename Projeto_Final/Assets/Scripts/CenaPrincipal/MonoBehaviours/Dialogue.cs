using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;

    public LayerMask playerLayer;
    public float radius;
    bool onRadius = false;

    private DialogueManager dc;

    private void Start() 
    {
        dc = FindObjectOfType<DialogueManager>();
    }

    private void FixedUpdate() 
    {
        Interact();
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && onRadius)
        {
            dc.Speech(profile, speechText, actorName);
        }
    }

    // Função que detecta quando o player interage com o npc
    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        onRadius = hit != null ? true : false;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
