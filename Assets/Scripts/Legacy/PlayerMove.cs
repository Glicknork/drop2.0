using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	// player's speed
	public float speed = 0.35f;


	// is the player facing left?
	bool facingLeft = true;

	// is the player moving horizontally?
    bool moving = false;

	// when the player just jumped
	float jumped;
	float jumpDelay = 0.66f;
	// how far the person moves horizontally when jumping
	public float jumpDistance = 0.1f;
    [HideInInspector]
	public bool jumping = false;


	// when the player just screamed
	float justScreamed;
	float screamDelay = 2f;

    // when the player just thumped the ground
    float justThumped;
    float thumpDelay = 0.5f;

    // the animator
    [HideInInspector]
    public Animator anim;

	Rigidbody2D playerBody;

    // the speed float used by the touchscreen buttons to set the player's velocity
    [HideInInspector]
    public float touchScreenSpeed;

	public AudioClip fallingScream;
	public AudioClip fallingThump;
	public AudioClip lazerJumpSound;
    public AudioClip pickupSound;
    AudioSource source;

	// is the player dead?
	[HideInInspector]
	public bool dead = false;

	// is the player in the win zone?
	[HideInInspector]
	public bool winning = false;
    // is this the last level and therefore should be returned to main menu?
    [HideInInspector]
    public bool finalWin = false;


    public Transform groundCheck1;
    public Transform groundCheck2;
    bool grounded1;
    bool grounded2;
    [HideInInspector]
    public bool grounded;

    // a transform below the ground checkers for determining if the thump sound should be played
    public Transform thumpCheck;
    bool fallThump;
    
    void Start () {
		anim = GetComponent<Animator> ();
		playerBody = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource> ();
	}

	
	void Update () {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        MoveCheck();
        JumpCheck();
#endif
        JumpCheck();

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        // the player velocity checker for using touchscreen buttons
        playerBody.velocity = new Vector3(touchScreenSpeed * speed, playerBody.velocity.y);
#endif

        Grounded();
        FallingPhysics();

        // check if the player is moving and set the bool accordingly
        if (playerBody.velocity.x > 0.1 || playerBody.velocity.x < -0.1)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

    }

    // linecasts from the player's transform to two ground checker transforms and sets their bools. there are two checkers to ensure the player doesn't get stuck on overhangs
    void Grounded() {
        grounded1 = Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"));
        grounded2 = Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded1 || grounded2){
            grounded = true;
        }
        else{
            grounded = false;
        }
    }

#if UNITY_STANDALONE || UNITY_WEBPLAYER
    void MoveCheck() {
                         
        // apply velocity to the player
        float move = Input.GetAxis("Horizontal");
        if (grounded && !dead && !winning && !jumping){
          playerBody.velocity = new Vector3(move * speed, playerBody.velocity.y);
        }
        // flip the sprite depending on direction
        if (move < 0 && !facingLeft && grounded)
        {
            FlipSprite();
        }
        else if (move > 0 && facingLeft && grounded)
        {
            FlipSprite();
        }

        // set the ball animation
        if ((Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right")) && !jumping){
            anim.SetBool("Ball", true);
        }
        else {
            anim.SetBool("Ball", false);
        }
    }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
    // movement and jump commands for touchscreen buttons
    public void TouchMoveRight() {
        if (grounded && !dead && !winning && !jumping)
        {
            touchScreenSpeed = 1f;
           
        }
     if (facingLeft && grounded)
        {
            FlipSprite();
        }
    }
    public void TouchMoveLeft(){
        if (grounded && !dead && !winning && !jumping)
        {
            touchScreenSpeed = -1f;
          
        }
    if (!facingLeft && grounded)
        {
            FlipSprite();
        }
    }
    public void TouchJump() {
        if (grounded && !dead && !winning && !dead)
        {
            if ((jumped + jumpDelay) < Time.time){
                jumping = true;
                anim.SetTrigger("Jump");
                jumped = Time.time;
                {
            StartCoroutine("JumpDelay");
                }
            }
        }
    }
#endif

 
    // flip the sprite
    void FlipSprite()
    {
        facingLeft = !facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    void JumpCheck() {
        // check the player hasn't recently jumped, then call the jump coroutine
        if (Input.GetKeyDown("space") && grounded && !winning && !dead)
        {


            if ((jumped + jumpDelay) < Time.time)
            {
                if (moving)
                {

                    anim.SetTrigger("BallToJump");
                    jumping = true;
                    jumped = Time.time;
                    StartCoroutine("JumpDelay");
                }


                if (!moving)
                {
                    anim.SetTrigger("Jump");
                    jumping = true;
                    jumped = Time.time;
                    StartCoroutine("JumpDelay");
                }
            }
        }
    }

    // wait a few miliseconds for the animation to run before jumping the player (but don't jump if after that time he is no longer grounded)
	IEnumerator JumpDelay(){
			yield return new WaitForSeconds (0.15f);
        if (grounded)
        {
            Jump();
        }
        else {
            jumping = false;
        }
	}

    // change the player's x transform position to instantaniously move him
    void Jump()
    {

        source.PlayOneShot(lazerJumpSound, 1f);
        if (!facingLeft)
        {
            playerBody.transform.position = new Vector3(playerBody.position.x + jumpDistance, playerBody.position.y);
            jumping = false;
        }
        if (facingLeft)
        {
            playerBody.transform.position = new Vector3(playerBody.position.x - jumpDistance, playerBody.position.y);
            jumping = false;
        }
    }


    void FallingPhysics() {
        fallThump = Physics2D.Linecast(transform.position, thumpCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded == false)
        {
           playerBody.velocity = new Vector2(0, playerBody.velocity.y);
            anim.SetBool("Falling", true);
            anim.SetBool("Ball", false);
        }
        if (grounded == true) {
            anim.SetBool("Falling", false);
        }
        // if the thump checker is activated when the player is falling play the thump sound
        if (grounded == false && fallThump == true){
            FallingThump();
            }            
        
    }


		// check that the player hasn't just screamed before calling a scream
	void Scream(){
		if ((justScreamed + screamDelay) < Time.time) {
			justScreamed = Time.time;
			source.PlayOneShot(fallingScream, 1f);
		}
	}

	// call a ground hitting sound
	void FallingThump(){
        if ((justThumped + thumpDelay) < Time.time){
            justThumped = Time.time;
            source.PlayOneShot(fallingThump, 1f);
        }
	}

	


	public void IncreaseSpeed(float increaseSpeedBy){
		speed += increaseSpeedBy;
        source.PlayOneShot(pickupSound, 1f);
    }

	public void IncreaseJump(float increaseJumpBy){
		jumpDistance += increaseJumpBy;
        source.PlayOneShot(pickupSound, 1f);
    }

	// all of the Trigger 2D events: hitting flames, being hit by acid, entering the win zone and passing through a falling screamer
	void OnTriggerEnter2D (Collider2D coll){
		if (coll.gameObject.tag == "Flames" || coll.gameObject.tag == "Acid") {
			playerBody.velocity = new Vector3 (0, 0);
			playerBody.gravityScale = 0;
			anim.SetTrigger ("Dead");
			dead = true;
			StartCoroutine ("LevelResetDelay");
				if (coll.gameObject.tag == "Acid"){
					Scream ();
				}
		}
		if (coll.gameObject.tag == "WinZone") {
			winning = true;
			anim.SetTrigger ("Victory");
			StartCoroutine ("LevelResetDelay");
		}
        if (coll.gameObject.tag == "FinalWin")
        {
            finalWin = true;
        }
            if (coll.gameObject.tag == "FallingScream") {
			Scream ();
		}
    }

  
    

   // wait a few seconds before resetting the level
    private IEnumerator LevelResetDelay()
    {
        yield return new WaitForSeconds(3);
        if (dead == true)
        {
            ResetLevel();
        }
        if (winning == true && finalWin == false)
        {
            NextLevel();
        }
        if (winning == true && finalWin == true)
        {
            QuitToTitle();
        }
    }

	// function that resets to title screen
	void ResetLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}

	void NextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	void QuitToTitle(){
		Application.LoadLevel(0);
	}
			
}





