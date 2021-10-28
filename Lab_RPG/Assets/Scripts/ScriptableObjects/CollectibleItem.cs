using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Coletável")]
public class CollectibleItem : ScriptableObject
{
    public string itemName;        // nome do item
    public Sprite sprite;          // imagem do item
    public int effectiveQuantity;  // quantidade efetiva que o item adiciona a algum atributo/conjunto
    public bool stackable;         // indica se um conjunto do item ocupa um espaço (true) ou mais (false) 
    public enum ItemType            // enumerados que indica os tipos disponíveis para os itens
    {
        MONEY,
        HEALTH,
        FOOD,
        RESOURCES
    }
    public ItemType itemType;       // indica o tipo do item para que esse seja assimilado de forma adequada
}
