using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public AudioSource source1;
    public AudioSource source2;

    public float maxMusicVol;
    public float startingVolume = 0.2f;
    public float volumeIncrease = 0.05f;
    public AudioClip gameMusic;
    public AudioClip victoryMusic;

    // cache the player
    PlayerController player;

    float victorySoundPlayed;
    float victorySoundDelay = 10f;

    // Use this for initialization
    void Start () {
        StartCoroutine("IncreaseMusic");
        source1.PlayOneShot(gameMusic, startingVolume);
        player = GD.player;
    }
	
	// Update is called once per frame
	void Update () {
        if (player.playerState == PlayerController.PlayerState.VICTORY)
        {
            source1.Stop();
            if ((victorySoundPlayed + victorySoundDelay) < Time.time)
            {
                victorySoundPlayed = Time.time;
                source2.PlayOneShot(victoryMusic, 0.5f);
            }
        }
    }

    IEnumerator IncreaseMusic()
    {
        if (source1.volume <= maxMusicVol)
        {
            source1.volume += volumeIncrease;
            yield return new WaitForSeconds(1);
            StartCoroutine("IncreaseMusic");
        }

    }
}
