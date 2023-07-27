using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : CollidableObject
{
    public float timeInteract;
    public Slider slider;
    public GameObject castingBar;
    public Item itemDrop;

    private bool z_Interacted = false;
    private float timeLeft;
    private float playerSpeed;

    private void FixedUpdate()
    {
        if (z_Interacted)
        {
            castingBar.SetActive(true);

            if (timeLeft > 0)
            {
                timeLeft -=Time.deltaTime;
                slider.value = (1-timeLeft/timeInteract)*100;
                PlayerMovement.instance.moveSpeed = 0f;
            }
            else
            {
                this.gameObject.SetActive(false);
                slider.value = 0;
                castingBar.SetActive(false);
                Inventory.instance.Add(itemDrop);
                PlayerMovement.instance.moveSpeed = playerSpeed;

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
            playerSpeed = PlayerMovement.instance.moveSpeed;
        }
    }
}
