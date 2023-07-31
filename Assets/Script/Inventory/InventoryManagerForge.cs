using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManagerForge : MonoBehaviour
{
    
    public GameObject inventoryItemPrefab;
    public GameObject inventoryUI;

    InventorySlot[] inventorySlots;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        inventorySlots = inventoryUI.GetComponentsInChildren<InventorySlot>();
        UpdateInventoryUI();
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DragScript itemInSlot = slot.GetComponentInChildren<DragScript>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < item.maxStack)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            DragScript itemInSlot = slot.GetComponentInChildren<DragScript>();
            if(itemInSlot == null) 
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab,slot.transform);
        DragScript dragScript = newItemGo.GetComponent<DragScript>();
        dragScript.InitialiseItem(item);
    }

    public void UpdateInventoryUI()
    {
        foreach (Item item in inventory.items)
        {
            AddItem(item);
        }
    }

    public void CleanInventoryUI()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.GetComponentInChildren<DragScript>() != null)
            {
                Destroy(slot.transform.GetChild(0).GetComponent<DragScript>().gameObject);
            }
        }
        UpdateInventoryUI();
    }
}
