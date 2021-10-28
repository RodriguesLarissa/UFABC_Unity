using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character   
{

    public int money;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible")) // verifica se é um item coletável
        {
            CollectItem(collision);
        }
    }
    /*
     * Essa função é responsável por receber um item coletável a partir de uma colisão e definir para ele
     * um processamento apropriado de acordo com o seu tipo
     */
    public void CollectItem(Collider2D collision) {

        CollectibleItem collectedItem = collision.gameObject.GetComponent<Collectible>().item;
        switch (collectedItem.itemType)  // verifica o tipo do item coletável para realizar o evento apropriado
        {
            case CollectibleItem.ItemType.MONEY:                                 // caso uma moeda seja coletada
                print("Você coletou " + collectedItem.effectiveQuantity + " " + collectedItem.itemName + "!");
                this.money++;
                print("Agora você tem " + money + " moedas!");
                collision.gameObject.SetActive(false);
                break;
            default:
                print("Ainda não há uma definição adequada para coletar itens do tipo " + collectedItem.itemType);
                collision.gameObject.SetActive(false);
                break;
        }
    }

}
