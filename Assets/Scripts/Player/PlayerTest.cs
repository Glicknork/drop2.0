using UnityEngine;
using System.Collections;

public class PlayerTest : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector3(3, rb.velocity.y);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.velocity = new Vector3(-3, rb.velocity.y);
        }
    }
}
