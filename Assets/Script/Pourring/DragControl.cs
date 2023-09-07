using UnityEngine;

public class DragControl : MonoBehaviour
{
    [SerializeField] internal ForgeMiniGamePourring forgeMiniGamePourring;

    [SerializeField] private float maxYrange = 4f;
    Vector3 difference = Vector3.zero;
    private bool droped = true;
    private bool wasDragOnce = false;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private GameObject crucible;

    private void OnMouseDown()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.y += transform.GetComponentInParent<Transform>().transform.position.y;
        droped = false;
    }

    private void OnMouseDrag()
    {
        
        Vector3 pos = new Vector3(0, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) - difference;
        pos.x = 0;
        pos.y = Mathf.Clamp(pos.y, -maxYrange, 0);
        transform.localPosition = pos;

        if (!wasDragOnce)
        {
            wasDragOnce = true;
            forgeMiniGamePourring.timerScript.StartCountdown();
        }
    }

    private void OnMouseUp()
    {
        droped = true;
    }

    public void FixedUpdate()
    {
        if (droped)
        {
            transform.Translate(new Vector3(0,-transform.localPosition.y) * speed,0);
            if (transform.localPosition.y == 0f)
            {
                droped = false;
            }
        }
        crucible.transform.localEulerAngles = new Vector3(0,0, -45 + transform.localPosition.y * (90/4f));
    }
}
