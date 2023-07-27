using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Zone");
        if(transform.childCount == 0)
        {
            DragScript dragScript = eventData.pointerDrag.GetComponent<DragScript>();
            dragScript.parentAfterDrag = transform;
        }
        /*
        if(eventData.pointerDrag != null)
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            SelectedItem.instance.SpawnItem(SelectedItem.instance.itemSelected);
        }*/
    }
}
