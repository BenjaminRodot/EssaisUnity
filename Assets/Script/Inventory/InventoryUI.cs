using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();
        }
        if((Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") != 0)&& inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
    }

    void UpdateUI()
    {
        int nbDifferentItem = 0;
        foreach (InventorySlot slot in slots)
        {
            slot.ClearSlot();
        }

        List<Item> tempItems = new List<Item>();
        foreach (Item item in inventory.items)
        {
            if (!tempItems.Contains(item))
            {
                tempItems.Add(item);
                nbDifferentItem++;
            }
            int i = tempItems.IndexOf(item);

            slots[i].AddItem(item);
        }
        for(int j = nbDifferentItem; j<itemsParent.childCount; j++)
        {
            itemsParent.GetChild(j).gameObject.SetActive(false);
        }
    }
}
