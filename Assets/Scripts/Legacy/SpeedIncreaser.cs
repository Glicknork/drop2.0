using UnityEngine;
using System.Collections;

public class SpeedIncreaser : MonoBehaviour {

	public float increasedSpeed = 0.6f;

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<PlayerMove>().IncreaseSpeed(increasedSpeed);
		
		}
	}
}