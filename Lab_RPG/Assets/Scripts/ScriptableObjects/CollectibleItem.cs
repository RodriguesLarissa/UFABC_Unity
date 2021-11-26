using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para armazenar as informações dos itens coletaveis no jogo, como corações e moedas.
/// </summary>

[CreateAssetMenu(menuName = "Item Colet�vel")]
public class CollectibleItem : ScriptableObject
{
    public string itemName;        // nome do item
    public Sprite sprite;          // imagem do item
    public int effectiveQuantity;  // quantidade efetiva que o item adiciona a algum atributo/conjunto
    public bool stackable;         // indica se um conjunto do item ocupa um espa�o (true) ou mais (false) 
    public enum ItemType            // enumerados que indica os tipos dispon�veis para os itens
    {
        MONEY,
        HEALTH,
        EMERALD,
        GREENMONEY,
        DIAMOND,
        RUBY,
        RESOURCES
    }
    public ItemType itemType;       // indica o tipo do item para que esse seja assimilado de forma adequada
}
