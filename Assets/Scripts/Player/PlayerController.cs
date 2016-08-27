using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour{

    public enum PlayerState
    {
        IDLE,
        ROLLING
    }

    public PlayerState playerState;

    public float standardSpeed;
    float currentSpeed;

    // components
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;

    // is the player facing left?
    bool facingLeft = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerState = PlayerState.IDLE;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {      

        // set the ball animation
        if (Input.GetKey("a") ||  Input.GetKey("left"))
        {
            rb.velocity = new Vector3(-standardSpeed, rb.velocity.y);           
            anim.SetBool("BallRolling", true);
            if (!facingLeft)
            {
                FlipSprite();
            }            
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("left"))
        {
            anim.SetBool("BallRolling", false);
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector3(standardSpeed, rb.velocity.y);
            anim.SetBool("BallRolling", true);
            if (facingLeft)
            {
                FlipSprite();
            }            
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
        {
            anim.SetBool("BallRolling", false);
        }
    }

    // flip the sprite
    void FlipSprite()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
