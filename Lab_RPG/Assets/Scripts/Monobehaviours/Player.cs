using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character   
{
    public Inventario inventarioPrefab;
    Inventario inventario;
    public HealthBar healthBarPrefab;
    HealthBar healthBar;
    public int money;
    void Start()
    {
        inventario = Instantiate(inventarioPrefab);
        healthPoints.value = initialhealthPoints;
        healthBar.caractere = this;
        healthBar = Instantiate(healthBarPrefab);
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
    public void CollectItem(Collider2D collision) 
    {
        CollectibleItem collectedItem = collision.gameObject.GetComponent<Collectible>().item;
        if (collectedItem != null)
        {
            bool shouldDissapear = false;
            // verifica o tipo do item colet�vel para realizar o evento apropriado
            switch (collectedItem.itemType)  
            {
                case CollectibleItem.ItemType.MONEY: // caso uma moeda seja coletada
                    shouldDissapear = inventario.AddItem(collectedItem);
                    print("Voc� coletou " + collectedItem.effectiveQuantity + " " + collectedItem.itemName + "!");
                    this.money++;
                    print("Agora voc� tem " + money + " moedas!");
                    break;
                case CollectibleItem.ItemType.HEALTH:
                    shouldDissapear = AjustePontosDano(collectedItem.effectiveQuantity);
                    break;
                default:
                    print("Ainda n�o h� uma defini��o adequada para coletar itens do tipo " + collectedItem.itemType);
                    break;
            }
            if (shouldDissapear) collision.gameObject.SetActive(false);
        }
    }

    public bool AjustePontosDano(int quantidade) 
    {
        if (healthPoints.value < maxHealthPoints)
        {
            healthPoints.value += quantidade;
            print("Ajustando Pontos Dano por: " + quantidade + ". Novo Valor = " + healthPoints.value);
            return true;
        } return false;
    }
}
