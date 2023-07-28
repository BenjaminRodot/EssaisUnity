using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {        
        DragScript dragScript = eventData.pointerDrag.GetComponent<DragScript>();
        Item itemDroped = dragScript.item;
        SelectedItem.instance.SpawnItem(itemDroped);
        Inventory.instance.items.Remove(dragScript.item);
        if (dragScript.count > 1)
        {
            dragScript.count--;
            dragScript.RefreshCount();
        }
        else
        {
            Destroy(dragScript.gameObject);
        }
        
    }
}
