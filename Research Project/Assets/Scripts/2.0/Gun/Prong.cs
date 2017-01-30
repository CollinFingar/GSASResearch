using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class Prong : GunScript {

	public GameObject shot;
	public Vector3 velocity = new Vector3();
	public Vector3 leftOffset = new Vector3();
	public Vector3 rightOffset = new Vector3();

	public float frequency = .2f;

	private float timeTillNextShot = 0f;
	private bool shooting = false;

	public GameObject leftBarrelLocation;
	public GameObject rightBarrelLocation;

	private AudioSource AS;

	// Use this for initialization
	void Start () {
		AS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		shooting = squeezingTrigger;
		if (shooting) {
			AutoShoot ();
		} else if (Time.time > timeTillNextShot + frequency && !shooting) {
			if (rightHand) {
				OVRInput.SetControllerVibration(0f,0f,OVRInput.Controller.RTouch);
			} else {
				OVRInput.SetControllerVibration(0f,0f,OVRInput.Controller.LTouch);
			}
		}
	}

	void AutoShoot(){
		if (Time.time > timeTillNextShot) {
			FireShot ();
			timeTillNextShot = Time.time + frequency;
		}
	}

	void FireShot(){
		GameObject newShotL = (GameObject)Instantiate (shot, leftBarrelLocation.transform.position, Quaternion.identity);
		newShotL.GetComponent<ProngShot> ().velocity = transform.right * 15;
		GameObject newShotR = (GameObject)Instantiate (shot, rightBarrelLocation.transform.position, Quaternion.identity);
		newShotR.GetComponent<ProngShot> ().velocity = transform.right * 15;

		if (rightHand) {
			OVRInput.SetControllerVibration (.5f, .5f, OVRInput.Controller.RTouch);
		} else {
			OVRInput.SetControllerVibration (.5f, .5f, OVRInput.Controller.LTouch);
		}
		AS.Play ();

	}
}
