using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    PlayerController player;
	
	void Start () {
        player = GD.player;
	}	
	

    void OnTriggerEnter2D(Collider2D coll)
    {
        player.playerState = PlayerController.PlayerState.IDLE;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        player.playerState = PlayerController.PlayerState.FALLING;
    }
}
