using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    #region Singleton
    public static SelectedItem instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of SelectedItem found !");
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion

    public Item itemSelected;
    public GameObject itemToClone;
    public List<GameObject> itemsDroped;

    private Camera mainCamera;
    private Vector3 mousePos;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void SpawnItem(Item item)
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        itemToClone.GetComponent<SpriteRenderer>().sprite = item.image;
        GameObject itemDroped = Instantiate(itemToClone, mousePos, Quaternion.identity);
        itemsDroped.Add(itemDroped);
    }

    public void CleanDroppedItem()
    {
        foreach (var item in itemsDroped)
        {
            Destroy(item);
        }
        itemsDroped.Clear();
    }
}
