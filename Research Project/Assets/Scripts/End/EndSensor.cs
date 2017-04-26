using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSensor : MonoBehaviour {

	public EndBeam eb;
	public AudioClip vibrateClip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Hand") {
			eb.StartEnd ();
			Hand hs = other.gameObject.GetComponent<Hand> ();
			if (hs.rightHand) {
				OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
				OVRHaptics.RightChannel.Preempt(clip);
			} else {
				OVRHapticsClip clip = new OVRHapticsClip (vibrateClip, 1);
				OVRHaptics.LeftChannel.Preempt(clip);
			}
		}
	}
}
