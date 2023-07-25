using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : CollidableObject
{
    public Transform UI_text;
    public float timeInteract;

    private bool z_Interacted = false;
    private float timeLeft;
    public Slider slider;
    public GameObject castingBar;
    private float progress;
    private float rate;

    private void FixedUpdate()
    {
        if (z_Interacted)
        {
            castingBar.SetActive(true);

            if (timeLeft > 0)
            {
                timeLeft -=Time.deltaTime;
                slider.value = (1-timeLeft/timeInteract)*100;
            }
            else
            {
                this.gameObject.SetActive(false);
                Debug.Log("INTERACT WITH " + name);
                UI_text.GetComponent<TMPro.TextMeshProUGUI>().text = (int.Parse(UI_text.GetComponent<TMPro.TextMeshProUGUI>().text) + 1).ToString();
                slider.value = 0;
                castingBar.SetActive(false);

            }
        }
    }

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
            slider.enabled = true;
            slider.value = 0f;
            timeLeft = timeInteract;
            z_Interacted = true;
        }
    }
}
