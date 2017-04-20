﻿using System.Collections;
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
	public GameObject ps1;
	public GameObject ps2;

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
		} else if (Time.time > timeTillNextShot + frequency && !shooting) {
			ps1.SetActive (false);
			ps2.SetActive (false);
		}
	}

	void AutoShoot(){
		if (Time.time > timeTillNextShot) {
			FireShot ();
			timeTillNextShot = Time.time + frequency;
		}
	}

	void FireShot(){
		ps1.SetActive (false);
		ps2.SetActive (false);
		GameObject newShotL = (GameObject)Instantiate (shot, leftBarrelLocation.transform.position, Quaternion.identity);
		newShotL.GetComponent<ProngShot> ().velocity = transform.right * 15;
		GameObject newShotR = (GameObject)Instantiate (shot, rightBarrelLocation.transform.position, Quaternion.identity);
		newShotR.GetComponent<ProngShot> ().velocity = transform.right * 15;

		if (rightHand) {
			OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
			OVRHaptics.RightChannel.Preempt(clip);
		} else {
			OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
			OVRHaptics.LeftChannel.Preempt(clip);
		}
		AS.Play ();
		ps1.SetActive (true);
		ps2.SetActive (true);
	}
}
