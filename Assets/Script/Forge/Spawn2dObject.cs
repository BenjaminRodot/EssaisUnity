using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn2dObject : MonoBehaviour
{
    public GameObject itemDroped;

    private Camera mainCamera;
    private Vector3 mousePos;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void SpawnItem(Item item)
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        itemDroped.GetComponent<SpriteRenderer>().sprite = item.image;
        Instantiate(itemDroped, mousePos, Quaternion.identity);
    }
}
