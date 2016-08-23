using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour {

    Rigidbody2D rb;

    public float moveSpeed;

    public Collider2D moveColliderOne;
    public Collider2D moveColliderTwo;
    

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveSpeed * (moveColliderTwo.gameObject.transform.position - transform.position).normalized;

    }
	
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll == moveColliderTwo) {
            rb.velocity = moveSpeed * (moveColliderOne.gameObject.transform.position - transform.position).normalized;
        }
        if (coll == moveColliderOne) {
            rb.velocity = moveSpeed * (moveColliderTwo.gameObject.transform.position - transform.position).normalized;
        }
    }
   
}