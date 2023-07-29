using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public bool IsCollided = false;

    void OnTriggerEnter2D(Collider2D targetCollider)
    {
        IsCollided = true;
    }

    void OnTriggerExit2D(Collider2D targetCollider)
    {
        IsCollided = false;
    }

    private void OnTriggerStay(Collider other)
    {
        IsCollided = true;
    }
}
