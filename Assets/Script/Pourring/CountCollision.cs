using UnityEngine;

public class CountCollision : MonoBehaviour
{
    private int count = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        count++;
    }

    public int GetCount()
    {
        return count;
    }
}
