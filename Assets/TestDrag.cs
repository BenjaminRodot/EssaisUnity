using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestDrag : MonoBehaviour
{
    [SerializeField] private float maxYrange = 4f;
    Vector2 difference = Vector2.zero;
    private bool droped = false;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private GameObject crucible;

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        difference.y += transform.GetComponentInParent<Transform>().transform.position.y;
        droped = false;
    }

    private void OnMouseDrag()
    {
        
        Vector2 pos = new Vector2(0, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) - difference;
        pos.x = 0;
        pos.y = Mathf.Clamp(pos.y, -maxYrange, 0);
        transform.localPosition = pos;
    }

    private void OnMouseUp()
    {
        droped = true;
    }

    public void FixedUpdate()
    {
        if (droped)
        {
            transform.Translate(new Vector2(0,-transform.localPosition.y) * speed);
            if (transform.localPosition.y == 0f)
            {
                droped = false;
            }
        }

        crucible.transform.localEulerAngles = new Vector3(0,0, -45 + transform.localPosition.y * (90/4f));
    }
}
