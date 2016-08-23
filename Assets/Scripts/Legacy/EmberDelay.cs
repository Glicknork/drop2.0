using UnityEngine;
using System.Collections;

public class EmberDelay : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		StartCoroutine ("StaggerAnimation");
	}

	IEnumerator StaggerAnimation(){
		yield return new WaitForSeconds(Random.Range (1f,2f));
		anim.SetTrigger ("Embers");
		
	}


}