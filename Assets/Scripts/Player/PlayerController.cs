using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour{

    public enum PlayerState
    {
        IDLE,
        ROLLING,
        FALLING,
        VICTORY
    }

    public PlayerState playerState;

    public float standardSpeed;
    float currentSpeed;
    public int currentCoins;

    // components
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;

    public Transform groundCheck1;
    public Transform groundCheck2;

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
        if (Grounded())
        {
            Movement();
        }
        if (!Grounded())
        {
            //Falling();
        }
    }

    bool Grounded()
    {
        bool grounded1 = Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Blocks"));
        bool grounded2 = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Blocks"));
        if(grounded1 || grounded2)
        {
            return true;
        }
        return false;
    }

    void Movement()
    {
        anim.SetBool("Falling", false);
        // set the ball animation
        if (Input.GetKey("a") ||  Input.GetKey("left"))
        {
            //rb.velocity = new Vector3(-standardSpeed, rb.velocity.y);     
            rb.AddForce(new Vector3(-standardSpeed, rb.velocity.y));      
            anim.SetBool("BallRolling", true);
            playerState = PlayerState.ROLLING;
            if (!facingLeft)
            {
                FlipSprite();
            }            
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("left"))
        {
            anim.SetBool("BallRolling", false);
            rb.velocity = new Vector3(0, rb.velocity.y);
            playerState = PlayerState.IDLE;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            //rb.velocity = new Vector3(standardSpeed, rb.velocity.y);
            rb.AddForce(new Vector3(standardSpeed, rb.velocity.y));
            anim.SetBool("BallRolling", true);
            playerState = PlayerState.ROLLING;
            if (facingLeft)
            {
                FlipSprite();
            }            
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
        {
            anim.SetBool("BallRolling", false);
            rb.velocity = new Vector3(0, rb.velocity.y);
            playerState = PlayerState.IDLE;
        }
    }

    void Falling()
    {
        rb.velocity = new Vector3(0, rb.velocity.y);
        anim.SetBool("BallRolling", false);
        anim.SetBool("Falling", true);
    }

    // flip the sprite
    void FlipSprite()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void IncreaseCoins(int ammount)
    {
        currentCoins += ammount;
    }

}
