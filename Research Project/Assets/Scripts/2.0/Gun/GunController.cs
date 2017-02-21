using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class GunController : MonoBehaviour {

	//Guns to cycle between
	public GameObject[] Guns;

	public GameObject[] rightGuns;
	public GameObject[] leftGuns;

	private int rightGunIndex = 0;
	private int leftGunIndex = 0;

	public float gunSwitchInterval = .5f;
	public float lastRightGunSwitchTime = 0f;
	public float lastLeftGunSwitchTime = 0f;

	public bool pullingTriggerRightGun = false;
	public bool pullingTriggerLeftGun = false;

	public bool switchingRightGun = false;
	public bool switchingLeftGun = false;

	public bool primaryHandRight = true;

	public GameObject rightHand;
	public GameObject leftHand;

	// Use this for initialization
	void Start () {
		leftGuns = new GameObject[leftHand.transform.childCount];
		int l = 0;
		foreach (Transform child in leftHand.transform) {
			leftGuns [l] = child.gameObject;
			if (l > 0) {
				leftGuns [l].SetActive (false);
			}
			l++;
		}

		rightGuns = new GameObject[rightHand.transform.childCount];
		int r = 0;
		foreach (Transform child in rightHand.transform) {
			rightGuns [r] = child.gameObject;
			if (r > 0) {
				rightGuns [r].SetActive (false);
			}
			r++;
			child.gameObject.GetComponent<GunScript> ().rightHand = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Array List of inputs
	//	0 - float - primaryTriggerSqueezeAmount
	//  1 - float - secondaryTriggerSqueezeAmount
	//	2 - float - primaryGripSqueezeAmount
	//  3 - float - secondaryGripSqueezeAmount
	public void ReceiveInput(ArrayList inputs){
		pullingTriggerRightGun = (float)inputs [0] > .2f;
		pullingTriggerLeftGun = (float)inputs [1] > .2f;
		RightTriggerHandler ();
		LeftTriggerHandler ();

		bool pgsa = (float)inputs [2] > .2f;
		bool sgsa = (float)inputs [3] > .2f;
		SenseGunSwitching (pgsa, sgsa);
	}

	public void RightTriggerHandler(){
		rightGuns [rightGunIndex].GetComponent<GunScript> ().squeezingTrigger = pullingTriggerRightGun;
	}

	public void LeftTriggerHandler(){
		leftGuns [leftGunIndex].GetComponent<GunScript> ().squeezingTrigger = pullingTriggerLeftGun;
	}


	void SenseGunSwitching(bool squeezingPrimary, bool squeezingSecondary){
		if (squeezingPrimary && Time.time > lastRightGunSwitchTime + gunSwitchInterval) {
			SwitchRightGun ();
			lastRightGunSwitchTime = Time.time;
		}
		if (squeezingSecondary && Time.time > lastLeftGunSwitchTime + gunSwitchInterval) {
			SwitchLeftGun ();
			lastLeftGunSwitchTime = Time.time;
		}
	}

	public void SwitchRightGun(){
		rightGuns [rightGunIndex].SetActive (false);
		if (rightGunIndex < rightGuns.Length - 1) {
			rightGunIndex++;
		} else {
			rightGunIndex = 0;
		}
		rightGuns [rightGunIndex].SetActive (true);
	}

	public void SwitchLeftGun(){
		leftGuns [leftGunIndex].SetActive (false);
		if (leftGunIndex < leftGuns.Length - 1) {
			leftGunIndex++;
		} else {
			leftGunIndex = 0;
		}
		leftGuns [leftGunIndex].SetActive (true);
	}

	public void SwitchPrimaryHand(){
		primaryHandRight = !primaryHandRight;
	}
}
