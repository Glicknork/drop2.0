using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player")
			Destroy (gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
