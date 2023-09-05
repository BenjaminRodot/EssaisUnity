using UnityEngine;

public class CountCollision : MonoBehaviour
{
    private int count = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        count++;
        Debug.Log(this.name + other.gameObject.name);
    }


    public int GetCount()
    {
        return count;
    }
}
