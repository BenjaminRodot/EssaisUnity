using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : CollidableObject
{   
   // public string sceneNameToLoad;

    private bool z_Interacted = false;
    

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
            ScenesManager.instance.LoadScene(ScenesManager.Scene.ForgeScene1);
            z_Interacted = true;
        }
    }
}
