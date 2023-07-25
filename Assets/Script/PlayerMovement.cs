using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Transform nom;

    public float moveSpeed = 40f;

    private Vector2 movement;

    private void Start()
    {
        GameObject castingBar = GameObject.Find("CastingBar");
        castingBar.SetActive(false);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * moveSpeed;

        animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector2(0f,180f)); //Flip le sprite pour regarder vers la gauche
            nom.rotation = Quaternion.Euler(new Vector2(0f, 0f));
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.rotation = Quaternion.Euler(new Vector2(0f, 0f)); //Flip le sprite pour regarder vers la droite
            nom.rotation = Quaternion.Euler(new Vector2(0f, 0f));
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
