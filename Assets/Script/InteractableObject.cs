using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : CollidableObject
{
    private bool z_Interacted = false;
    public Transform UI_text;

    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKey(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        if (!z_Interacted)
        {
            z_Interacted = true;
            this.gameObject.SetActive(false);
            Debug.Log("INTERACT WITH " + name);
            UI_text.GetComponent<TMPro.TextMeshProUGUI>().text = (int.Parse(UI_text.GetComponent<TMPro.TextMeshProUGUI>().text) +1).ToString();
        }
    }
}
