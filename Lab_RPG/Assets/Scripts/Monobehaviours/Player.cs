using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character   
{
    public Inventario inventarioPrefab; // Armazena o prefab do Inventário
    Inventario inventario;              // Referência ao GameObject Inventário pertencente ao Player
    public HealthBar healthBarPrefab;   // Armazena o prefab da HealthBar do Player
    HealthBar healthBar;                // Referência ao GameObject HealthBar pertencente ao Player
    public int money;                   // TESTE: armazenar moedas
    public int totalColetaveis = 0;
    public PontosDano healthPoints; // Quantidade atual de pontos de vida

    //Define o que ocorre quando o player toma dano, por exemplo animação de flickr na cor e perda de HP
    public override IEnumerator DanoCaractere(int dano, float intervalo) 
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            healthPoints.value = healthPoints.value - dano;
            if (healthPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else
            {
                break;
            }
        }
    }

    /*
     * Método responsável por destruir os GameObjects associados ao Player quando esse morre
     */
    public override void KillCharacter()
    {
        SceneManager.LoadScene("Tela_Derrota");
    }

    /*
     * Método responsável por reiniciar os GameObjects associados ao Player: Inventário e HealthBar
     */
    public override void ResetCharacter()
    {
        inventario = Instantiate(inventarioPrefab);
        healthPoints.value = initialhealthPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
    }

    void Start()
    {
        inventario = Instantiate(inventarioPrefab);
        healthPoints.value = initialhealthPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
     * Método associado às colisões do Player: sua interação com outros GameObjects em cena
     */
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
                    this.totalColetaveis++;
                    print("Agora voc� tem " + money + " moedas!");
                    break;
                case CollectibleItem.ItemType.GREENMONEY: // caso uma moeda verde seja coletada
                    shouldDissapear = inventario.AddItem(collectedItem);
                    print("Voc� coletou " + collectedItem.effectiveQuantity + " " + collectedItem.itemName + "!");
                    this.money++;
                    this.totalColetaveis++;
                    print("Agora voc� tem " + money + " moedas verdes!");
                    break;
                case CollectibleItem.ItemType.EMERALD: // caso uma esmeralda seja coletada
                    shouldDissapear = inventario.AddItem(collectedItem);
                    print("Voc� coletou " + collectedItem.effectiveQuantity + " " + collectedItem.itemName + "!");
                    this.money++;
                    this.totalColetaveis++;
                    print("Agora voc� tem " + money + " esmeraldas!");
                    break;
                case CollectibleItem.ItemType.DIAMOND: // caso um diamante seja coletada
                    shouldDissapear = inventario.AddItem(collectedItem);
                    print("Voc� coletou " + collectedItem.effectiveQuantity + " " + collectedItem.itemName + "!");
                    this.money++;
                    this.totalColetaveis++;
                    print("Agora voc� tem " + money + " diamantes!");
                    break;
                case CollectibleItem.ItemType.RUBY: // caso um ruby seja coletada
                    shouldDissapear = inventario.AddItem(collectedItem);
                    print("Voc� coletou " + collectedItem.effectiveQuantity + " " + collectedItem.itemName + "!");
                    this.money++;
                    this.totalColetaveis++;
                    print("Agora voc� tem " + money + " rubis!");
                    break;
                case CollectibleItem.ItemType.HEALTH:
                    shouldDissapear = AjustePontosDano(collectedItem.effectiveQuantity);
                    break;
                default:
                    print("Ainda n�o h� uma defini��o adequada para coletar itens do tipo " + collectedItem.itemType);
                    break;
            }
            if (shouldDissapear) collision.gameObject.SetActive(false);
            if (this.totalColetaveis == 10) SceneManager.LoadScene("Tela_Vitoria");
        }
    }

    /*
     * Método responsável por atualizar a quantidade de Pontos de Vida do Player
     */ 
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
