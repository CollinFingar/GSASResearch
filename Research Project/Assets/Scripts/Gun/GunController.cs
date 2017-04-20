using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class GunController : MonoBehaviour {

	//Guns to cycle between
	public GameObject[] Guns;


	public bool pullingTriggerRightGun = false;
	public bool pullingTriggerLeftGun = false;

	public bool rightSwitchRelease = false;
	public bool leftSwitchRelease = false;

	public bool primaryHandRight = true;

	public GameObject rightHand;
	public GameObject leftHand;

	public GameObject rightHandObject;
	public GameObject rightProngGun;
	public GameObject rightShotgun;
	public GameObject rightOpenBlaster;
	public GameObject rightPlaceholder;
	public GameObject rightLauncher;
	public GameObject leftHandObject;
	public GameObject leftProngGun;
	public GameObject leftShotgun;
	public GameObject leftOpenBlaster;
	public GameObject leftPlaceholder;
	public GameObject leftLauncher;

	public bool grippingRightGun = false;
	public bool grippingLeftGun = false;
	public bool rightPermenant = true;
	public bool leftPermenant = true;
	public GameObject currentRightGun;
	public GameObject currentLeftGun;

	public GameObject presetRightGun;
	public GameObject presetLeftGun;
	private bool rightProngGunSelected = true;
	private bool leftProngGunSelected = true;

	public bool rightHandInSpawner = false;
	public bool leftHandInSpawner = false;

	private int rightPickupValue = 1;
	private int leftPickupValue = 1;
	//1 = Open Blaster
	//2 = Grenade Launcher
	//3 = ???

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Array List of inputs
	//	0 - float - primaryTriggerSqueezeAmount
	//  1 - float - secondaryTriggerSqueezeAmount
	//	2 - float - primaryGripSqueezeAmount
	//  3 - float - secondaryGripSqueezeAmount
	//  4 - bool  - switchingPrimary
	//  5 - bool  - switchingSecondary
	public void ReceiveInput(ArrayList inputs){
		bool switchingPrimary = (bool)inputs [4];
		bool switchingSecondary = (bool)inputs [5];
		if (switchingPrimary && !rightSwitchRelease) {
			SwitchRightGun ();
		} else if(!switchingPrimary && rightSwitchRelease){
			rightSwitchRelease = false;
		}
		if (switchingSecondary && !leftSwitchRelease) {
			SwitchLeftGun ();
		} else if(!switchingSecondary && leftSwitchRelease){
			leftSwitchRelease = false;
		}

		bool pgsa = (float)inputs [2] > .2f;
		bool sgsa = (float)inputs [3] > .2f;
		SenseGrip (pgsa, sgsa);

		pullingTriggerRightGun = (float)inputs [0] > .2f;
		pullingTriggerLeftGun = (float)inputs [1] > .2f;
		RightTriggerHandler ();
		LeftTriggerHandler ();
	}

	public void RightTriggerHandler(){
		if (grippingRightGun) {
			currentRightGun.GetComponent<GunScript>().squeezingTrigger = pullingTriggerRightGun;
		}
	}

	public void LeftTriggerHandler(){
		if (grippingLeftGun) {
			currentLeftGun.GetComponent<GunScript>().squeezingTrigger = pullingTriggerLeftGun;
		}
	}


	void SenseGrip(bool squeezingPrimary, bool squeezingSecondary){
		if (squeezingPrimary) {
			if (!grippingRightGun) {
				//SENSE IF PICKING UP GUN OR GENERATING
				rightHandInSpawner = rightHandObject.GetComponent<Hand> ().handInSpawner;
				if (rightHandInSpawner) {
					rightPickupValue = rightHandObject.GetComponent<Hand> ().spawnerValue;
					EquipPickup (true);
					rightPermenant = false;
					rightHandObject.GetComponent<Hand> ().handInSpawner = false;
				} else {
					currentRightGun = presetRightGun;
					currentRightGun.SetActive (true);
					rightPermenant = true;
				}
				rightHandObject.SetActive (false);
				grippingRightGun = true;
			}
		} else {
			if (grippingRightGun) {
				if (rightPermenant) {
					currentRightGun.SetActive (false);
				} else {
					//GET RID OF CURRENT PICKED UP GUN
					currentRightGun.SetActive (false);
					currentRightGun = presetRightGun;
				}
				rightHandObject.SetActive (true);
				rightHandObject.GetComponent<Hand> ().resetDigitColor ();
				grippingRightGun = false;
			}
		}
		if (squeezingSecondary) {
			if (!grippingLeftGun) {
				//SENSE IF PICKING UP GUN OR GENERATING
				leftHandInSpawner = leftHandObject.GetComponent<Hand> ().handInSpawner;
				if (leftHandInSpawner) {
					leftPickupValue = leftHandObject.GetComponent<Hand> ().spawnerValue;
					EquipPickup (false);
					leftPermenant = false;
					leftHandObject.GetComponent<Hand> ().handInSpawner = false;
				} else {
					currentLeftGun = presetLeftGun;
					currentLeftGun.SetActive (true);
					leftPermenant = true;
				}
				leftHandObject.SetActive (false);
				grippingLeftGun = true;
			}
		} else {
			if (grippingLeftGun) {
				if (leftPermenant) {
					currentLeftGun.SetActive (false);
				} else {
					//GET RID OF CURRENT PICKED UP GUN
					currentLeftGun.SetActive (false);
					currentLeftGun = presetLeftGun;
				}
				leftHandObject.SetActive (true);
				leftHandObject.GetComponent<Hand> ().resetDigitColor ();
				grippingLeftGun = false;
			}
		}
	}

	public void SwitchRightGun(){
		if (rightProngGunSelected) {
			rightProngGunSelected = false;
			presetRightGun = rightShotgun;
		} else {
			rightProngGunSelected = true;
			presetRightGun = rightProngGun;
		}
		if (grippingRightGun) {
			if (rightPermenant) {
				currentRightGun.SetActive (false);
			} else {
				//GET RID OF CURRENT PICKED UP GUN
				currentRightGun.SetActive (false);
			}
			currentRightGun = presetRightGun;
			currentRightGun.SetActive (true);
		}
		rightHandObject.GetComponent<Hand> ().ChangeGun ();
		rightSwitchRelease = true;
	}

	public void SwitchLeftGun(){
		if (leftProngGunSelected) {
			leftProngGunSelected = false;
			presetLeftGun = leftShotgun;
		} else {
			leftProngGunSelected = true;
			presetLeftGun = leftProngGun;
		}
		if (grippingLeftGun) {
			if (leftPermenant) {
				currentLeftGun.SetActive (false);
			} else {
				//GET RID OF CURRENT PICKED UP GUN
				currentLeftGun.SetActive (false);
			}
			currentLeftGun = presetLeftGun;
			currentLeftGun.SetActive (true);
		}
		leftHandObject.GetComponent<Hand> ().ChangeGun ();
		leftSwitchRelease = true;
	}

	public void EquipPickup(bool right){
		if (right) {
			if (rightPickupValue == 1) {
				currentRightGun = rightOpenBlaster;
			} else if (rightPickupValue == 2) {
				currentRightGun = rightLauncher;
			} else if (rightPickupValue == 3) {
				currentRightGun = rightOpenBlaster;
			}
			currentRightGun.SetActive (true);
		} else {
			if (leftPickupValue == 1) {
				currentLeftGun = leftOpenBlaster;
			} else if (leftPickupValue == 2) {
				currentLeftGun = leftLauncher;
			} else if (leftPickupValue == 3) {
				currentLeftGun = leftOpenBlaster;
			}
			currentLeftGun.SetActive (true);
		}
	}

	public void SwitchPrimaryHand(){
		primaryHandRight = !primaryHandRight;
	}
}
