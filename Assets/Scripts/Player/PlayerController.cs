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
    public Animator anim;
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
        MoveCheck();
    }

    void MoveCheck()
    {
        /*
        // apply velocity to the player
        float moveDir = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector3(moveDir * standardSpeed, rb.velocity.y);
        
        // flip the sprite depending on direction
        if (moveDir < 0 && !facingLeft)
        {
            FlipSprite();
        }
        else if (moveDir > 0 && facingLeft)
        {
            FlipSprite();
        }*/

        // set the ball animation
        if (Input.GetKey("a") ||  Input.GetKey("left"))
        {
            rb.velocity = new Vector3(-standardSpeed, rb.velocity.y);
            anim.SetTrigger("IntoBall");
            anim.SetBool("BallRolling", true);
            if (!facingLeft)
            {
                FlipSprite();
            }
        }
        else
        {
            anim.SetBool("BallRolling", false);
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector3(standardSpeed, rb.velocity.y);
            anim.SetTrigger("IntoBall");
            anim.SetBool("BallRolling", true);
            if (facingLeft)
            {
                FlipSprite();
            }
        }
        else
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
