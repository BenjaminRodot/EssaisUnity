using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollow : MonoBehaviour
{
    public float speed;
    public float distanceMax;
    public Animator animator;
    public Transform nom;

    private Transform target;

    private void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        if (Vector2.Distance(this.transform.position, target.position) > distanceMax)
        {
            Vector2 vector2 = target.position - transform.position;

            animator.SetFloat("Speed", Mathf.Abs(vector2.x) + Mathf.Abs(vector2.y));

            if (vector2.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(new Vector2(0f, 180f)); //Flip le sprite pour regarder vers la gauche
                nom.rotation = Quaternion.Euler(new Vector2(0f, 0f));
            }
            if (vector2.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(new Vector2(0f, 0f)); //Flip le sprite pour regarder vers la droite
                nom.rotation = Quaternion.Euler(new Vector2(0f, 0f));
            }

            this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        }
        else animator.SetFloat("Speed", 0);
    }
}
