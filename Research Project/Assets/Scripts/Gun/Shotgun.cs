
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Shotgun : GunScript {

	public GameObject shot;
	public Vector3 velocity = new Vector3();
	public Vector3 offset = new Vector3();

	public float frequency = 1f;

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
		}
		if (Time.time > timeTillNextShot - frequency*.5f) {
		}
	}

	void AutoShoot(){
		if (Time.time > timeTillNextShot) {
			FireShot ();
			timeTillNextShot = Time.time + frequency;
		}
	}

	void FireShot(){
		//ps.SetActive (false);
		GameObject[] shots = new GameObject[8];
		for (int i = 0; i < 8; i++) {
			GameObject newShot = (GameObject)Instantiate (shot, barrelLocation.transform.position, Quaternion.identity);
			Vector3 forwardDirection = transform.forward * 15;
			Vector3 upDirection = transform.up * Random.Range(-2f, 2f);
			Vector3 rightDirection = transform.right * Random.Range(-2f, 2f);

			newShot.GetComponent<ShotgunShot> ().velocity = forwardDirection + upDirection + rightDirection;
		}

		if (rightHand) {
			OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
			OVRHaptics.RightChannel.Preempt(clip);
		} else {
			OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
			OVRHaptics.LeftChannel.Preempt(clip);
		}
		AS.Play ();
		Vector3 pTransform = transform.position + transform.forward * .3f;
		GameObject particle = (GameObject)Instantiate (ps, pTransform, this.transform.rotation);
		Destroy (particle, 1.0f); //destroy particlesystem object after 2 seconds.
	}
}
