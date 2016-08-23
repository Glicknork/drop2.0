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

	public AudioSource source;
	public AudioSource source2;
	public float maxMusicVol;
	public float startingVolume = 0.2f;
	public float volumeIncrease = 0.05f;
	public AudioClip gameMusic;
	public AudioClip victoryMusic;

	float victorySoundPlayed;
	float victorySoundDelay = 10f;

	// cache the player
	public PlayerMove player;


	void Start(){
		cam = GetComponent<Camera> ();
		// StartCoroutine ("ChangeCamSize");
		//source = GetComponent<AudioSource> ();
		StartCoroutine ("IncreaseMusic");
		source.PlayOneShot (gameMusic, startingVolume);
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
		if (player.winning == true){
			source.Stop();
			if ((victorySoundPlayed + victorySoundDelay) < Time.time) {
				victorySoundPlayed = Time.time;
				source2.PlayOneShot (victoryMusic, 0.5f);
			}
		}
		
	}

	IEnumerator ChangeCamSize(){
		if (cam.orthographicSize >= cameraSize) {
			cam.orthographicSize -= zoomSpeed;
			yield return new WaitForSeconds (0.1f);
			StartCoroutine ("ChangeCamSize");
		}
	}

	IEnumerator IncreaseMusic(){
		if (source.volume <= maxMusicVol) {
			source.volume += volumeIncrease;
			yield return new WaitForSeconds (1);
			StartCoroutine ("IncreaseMusic");
		}
	
	}

}