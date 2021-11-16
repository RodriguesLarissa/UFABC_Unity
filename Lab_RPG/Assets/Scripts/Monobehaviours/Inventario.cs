using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject slotPrefab;
    public const int numSlots = 5;
    Image[] itemImagens = new Image[numSlots];
    CollectibleItem[] items = new CollectibleItem[numSlots];
    GameObject[] slots = new GameObject[numSlots];

    // Start is called before the first frame update
    void Start()
    {
        CriaSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CriaSlots()
    {
        if (slotPrefab != null)
        {
            for(int i = 0; i < numSlots; i ++)
            {
                GameObject novoSlot = Instantiate(slotPrefab);
                novoSlot.name = "ItemSlot_" + i;
                novoSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = novoSlot;
                itemImagens[i] = novoSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    public bool AddItem(CollectibleItem itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            {
                items[i].effectiveQuantity = items[i].effectiveQuantity + 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                quantidadeTexto.enabled = true;
                quantidadeTexto.text = items[i].effectiveQuantity.ToString();
                return true;
            }
            if (items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                items[i].effectiveQuantity = 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                quantidadeTexto.enabled = true;
                quantidadeTexto.text = items[i].effectiveQuantity.ToString();
                itemImagens[i].sprite = itemToAdd.sprite;
                itemImagens[i].enabled = true;
                return true;
            }
        }
        return false;
    }
}
