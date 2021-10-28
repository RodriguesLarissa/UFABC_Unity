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
        if (collision.gameObject.CompareTag("Collectible")) // verifica se � um item colet�vel
        {
            CollectItem(collision);
        }
    }
    /*
     * Essa fun��o � respons�vel por receber um item colet�vel a partir de uma colis�o e definir para ele
     * um processamento apropriado de acordo com o seu tipo
     */
    public void CollectItem(Collider2D collision) {

        CollectibleItem collectedItem = collision.gameObject.GetComponent<Collectible>().item;
        switch (collectedItem.itemType)  // verifica o tipo do item colet�vel para realizar o evento apropriado
        {
            case CollectibleItem.ItemType.MONEY:                                 // caso uma moeda seja coletada
                print("Voc� coletou " + collectedItem.effectiveQuantity + " " + collectedItem.itemName + "!");
                this.money++;
                print("Agora voc� tem " + money + " moedas!");
                collision.gameObject.SetActive(false);
                break;
            default:
                print("Ainda n�o h� uma defini��o adequada para coletar itens do tipo " + collectedItem.itemType);
                collision.gameObject.SetActive(false);
                break;
        }
    }

}
