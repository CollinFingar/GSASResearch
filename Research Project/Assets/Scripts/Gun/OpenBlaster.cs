using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBlaster : GunScript {
	
	public GameObject shot;
	public Vector3 velocity = new Vector3();
	public Vector3 offset = new Vector3();

	public float frequency = 2f;

	private float timeTillNextShot = 0f;
	private bool shooting = false;

	public GameObject barrelLocation;

	private AudioSource AS;
	public GameObject ps;

	public AudioClip vibrateClip;

	// Use this for initialization
	void Start () {
		AS = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		shooting = squeezingTrigger;
		if (shooting) {
			AutoShoot ();
		} else if (Time.time > timeTillNextShot + frequency/2 &&!shooting) {
			ps.SetActive (false);
		}
	}

	void AutoShoot(){
		if (Time.time > timeTillNextShot) {
			FireShot ();
			timeTillNextShot = Time.time + frequency;
		}
	}

	void FireShot(){
		ps.SetActive (false);
		GameObject newShot = (GameObject)Instantiate (shot, barrelLocation.transform.position, Quaternion.identity);
		newShot.GetComponent<BlasterShot> ().velocity = transform.up * -15;

		if (rightHand) {
			OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
			OVRHaptics.RightChannel.Preempt(clip);
		} else {
			OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
			OVRHaptics.LeftChannel.Preempt(clip);
		}
		AS.Play ();
		ps.SetActive (true);
	}
}
