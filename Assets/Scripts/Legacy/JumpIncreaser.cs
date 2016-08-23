using UnityEngine;
using System.Collections;

public class JumpIncreaser : MonoBehaviour {

	public float increasedJump = 0.6f;
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<PlayerMove>().IncreaseJump(increasedJump);
			
		}
	}
}
