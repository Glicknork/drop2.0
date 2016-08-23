using UnityEngine;
using System.Collections;

public class MovingKiller : MonoBehaviour
{

    Rigidbody2D rb;

    public float moveSpeed;

    public Collider2D moveColliderOne;
    public Collider2D moveColliderTwo;
    public Collider2D moveColliderThree;
    public Collider2D moveColliderFour;
    public Collider2D moveColliderFive;
    public Collider2D moveColliderSix;


    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveSpeed * (moveColliderOne.gameObject.transform.position - transform.position).normalized;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll == moveColliderOne)
        {
            rb.velocity = moveSpeed * (moveColliderTwo.gameObject.transform.position - transform.position).normalized;
        }
        if (coll == moveColliderTwo)
        {
            rb.velocity = moveSpeed * (moveColliderThree.gameObject.transform.position - transform.position).normalized;
        }
        if (coll == moveColliderThree)
        {
            rb.velocity = moveSpeed * (moveColliderFour.gameObject.transform.position - transform.position).normalized;
        }
        if (coll == moveColliderFour)
        {
            rb.velocity = moveSpeed * (moveColliderFive.gameObject.transform.position - transform.position).normalized;
        }
        if (coll == moveColliderFive)
        {
            rb.velocity = moveSpeed * (moveColliderSix.gameObject.transform.position - transform.position).normalized;
        }
        if (coll == moveColliderSix)
        {
            rb.velocity = moveSpeed * (moveColliderOne.gameObject.transform.position - transform.position).normalized;
        }
   
    }

}