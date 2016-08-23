using UnityEngine;
using System.Collections;

public class FlameDesync : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		StartCoroutine ("FlameDelay");	
	}

	IEnumerator FlameDelay(){
		yield return new WaitForSeconds (Random.Range (1f,2f));
		anim.SetTrigger ("FlameStart");

	}

	void Update(){

		/*if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}
		}*/
	}

}
