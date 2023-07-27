using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image icon;
    public TextMeshProUGUI quantite;
    public Item item;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            DragScript dragScript = eventData.pointerDrag.GetComponent<DragScript>();
            dragScript.parentAfterDrag = transform;
        }
    }
    
    public void AddItem(Item newItem)
    {
        item = newItem;
        quantite.text=(int.Parse(quantite.text)+1).ToString();

        icon.sprite = item.image;
        icon.enabled = true;
    }

    public void AddItem(Item newItem, int quantite)
    {
        for(int i = 0; i < quantite; i++)
        {
            AddItem(newItem);
        }
    }

    public void ClearSlot()
    {
        item = null;
        quantite.text = "0";

        icon.sprite = null;
        icon.enabled = false;
        this.gameObject.SetActive(true);
    }
}
