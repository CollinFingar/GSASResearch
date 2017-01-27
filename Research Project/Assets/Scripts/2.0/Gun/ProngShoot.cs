using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class ProngShoot : MonoBehaviour {

	public GameObject shot;
	public Vector3 velocity = new Vector3();
	public Vector3 leftOffset = new Vector3();
	public Vector3 rightOffset = new Vector3();

	public float frequency = .2f;

	private float timeTillNextShot = 0f;
	private bool shooting = false;

	public GameObject leftBarrelLocation;
	public GameObject rightBarrelLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float triggerSqueezeAmount = OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger);
		if (triggerSqueezeAmount > .2) {
			shooting = true;	
		} else {
			shooting = false;
		}
		if (shooting) {
			AutoShoot ();
		} else if (Time.time > timeTillNextShot + frequency && !shooting) {
			OVRInput.SetControllerVibration(0f,0f,OVRInput.Controller.RTouch);
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
		//OVRHapticsClip hapticsClip = new OVRHapticsClip ();
		//hapticsClip.Samples.set
		//OVRHaptics.
		OVRInput.SetControllerVibration(.5f,.3f,OVRInput.Controller.RTouch);
	}
}
