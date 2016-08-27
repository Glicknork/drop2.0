using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public float cameraSize = 0.2f;
	public float zoomSpeed = 0.0001f;

	Camera cam;
	public float cameraOffset = 0.2f;

	// cache the player
	public PlayerController player;

	void Start(){
		cam = GetComponent<Camera> ();
		// StartCoroutine ("ChangeCamSize");			
		}
	
	// Update is called once per frame
	void Update () 
	{
		if (target)
		{
			Vector3 point = cam.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, cameraOffset, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

	IEnumerator ChangeCamSize(){
		if (cam.orthographicSize >= cameraSize) {
			cam.orthographicSize -= zoomSpeed;
			yield return new WaitForSeconds (0.1f);
			StartCoroutine ("ChangeCamSize");
		}
	}

	

}