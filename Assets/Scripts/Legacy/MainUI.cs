using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {

	// grab tutorial text components
	public Text hurry;
	public Text jump;

    public Button touchLeft;
    public Button touchRight;
    public Button touchJump;

	public Text timer;

	public int timeBeforeHurryText = 2;
	public int timeHurryTextLasts = 5;
	public int timeBeforeJumpText = 7;
	public int timeJumpTextLasts = 7;

	public PlayerMove player;

	float startTime;
	float currentTime;
    float roundedTime;

	public CameraFollow camera;

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
    bool leftPressed;
    bool rightPressed;
#endif



    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevel == 1)
        {
            StartCoroutine("Intro");
        }
        startTime = Time.time - startTime;
        AudioListener.pause = false;

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        Cursor.visible = false;
        touchLeft.gameObject.SetActive(false);
        touchRight.gameObject.SetActive(false);
        touchJump.gameObject.SetActive(false);
#endif

    }
	
	// Update is called once per frame
	void Update () {
		currentTime = Time.time - startTime;
        roundedTime = Mathf.Round(currentTime * 100f) / 100f;

        if (Input.GetKeyDown (KeyCode.P)) {
            Pause();
			
		}
		// quit to menu on pressing Escape
		if (Input.GetKeyDown (KeyCode.Escape))
            Application.LoadLevel(0);

        if (player.winning == false && !player.dead)
			timer.text = roundedTime.ToString();

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        // call the left and right movement functions if the button bools are pressed down
        UILeft();
        UIRight();
        
#endif
    }

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
    public void LeftDown() {
        leftPressed = true;
        
    }
   public void LeftUp(){
        leftPressed = false;
        player.anim.SetBool("Ball", false);
        player.touchScreenSpeed = 0f;
    }

   public void RightDown(){
        rightPressed = true;
        
    }
   public void RightUp(){
        rightPressed = false;
        player.anim.SetBool("Ball", false);
        player.touchScreenSpeed = 0f;
    }

    // UI references for the touchscreen buttons, linking to the player movement functions
    void UILeft()
    {
        if (leftPressed == true) {
        player.TouchMoveLeft();
            if (leftPressed && player.jumping == false && player.grounded)
            {
                player.anim.SetBool("Ball", true);
            }
            else
            {
                player.anim.SetBool("Ball", false);
            }

        }
    }
    public void UIRight(){
        if (rightPressed == true) {
        player.TouchMoveRight();
            if (rightPressed && player.jumping == false && player.grounded)
            {
                player.anim.SetBool("Ball", true);
            }
            else
            {
                player.anim.SetBool("Ball", false);
            }

        }
    }
    public void UIJump(){
        player.TouchJump();
        player.anim.SetBool("Ball", false);
        player.touchScreenSpeed = 0f;
    }
#endif

    public void Pause() {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }

    // turns health and parasite tutorial text game objects on and off
    IEnumerator Intro()
	{
		yield return new WaitForSeconds (timeBeforeHurryText);
		hurry.gameObject.SetActive(true);
		yield return new WaitForSeconds (timeHurryTextLasts);
		hurry.gameObject.SetActive(false);
		yield return new WaitForSeconds (timeBeforeJumpText);
		jump.gameObject.SetActive(true);
		yield return new WaitForSeconds (timeJumpTextLasts);
		jump.gameObject.SetActive(false);
	}


}
