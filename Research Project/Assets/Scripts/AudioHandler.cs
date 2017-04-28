using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	public AudioSource AS;

	public AudioClip MenuMusic;
	public AudioClip MissionMusic;

	// Use this for initialization
	void Start () {
		AudioHandler[] ahs = FindObjectsOfType<AudioHandler> ();
		if (ahs.Length == 1) {
			DontDestroyOnLoad (this.gameObject);
			AS = GetComponent<AudioSource> ();
			LoadMenuMusic ();
		} else {
			Destroy (gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadMenuMusic(){
		AS.volume = 1f;
		AS.clip = MenuMusic;
		AS.Play ();
	}

	public void LoadMissionMusic(){
		AS.volume = .6f;
		AS.clip = MissionMusic;
		AS.Play ();
	}
}
