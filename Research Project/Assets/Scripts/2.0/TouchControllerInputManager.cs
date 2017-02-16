using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class TouchControllerInputManager : MonoBehaviour {

	public float triggerSqueezeAmountRight = 0f;
	public float triggerSqueezeAmountLeft = 0f;
	public float gripSqueezeAmountRight = 0f;
	public float gripSqueezeAmountLeft = 0f;
	 
	public Vector2 analogStickRight = Vector2.zero;
	public Vector2 analogStickLeft = Vector2.zero;

	public bool aButtonPressed = false;
	public bool bButtonPressed = false;
	public bool xButtonPressed = false;
	public bool yButtonPressed = false;

	public bool startButtonPressed = false;

	public GunController GC;
	public Navigate NAV;
	public UIHandler UI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SenseTouchButtons ();
		UpdateGunController ();
	}

	void SenseTouchButtons(){
		triggerSqueezeAmountRight = OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
		triggerSqueezeAmountLeft = OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

		gripSqueezeAmountRight = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
		gripSqueezeAmountLeft = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

		analogStickRight = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
		analogStickLeft = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

		aButtonPressed = OVRInput.Get (OVRInput.Button.One);
		bButtonPressed = OVRInput.Get (OVRInput.Button.Two);
		xButtonPressed = OVRInput.Get (OVRInput.Button.Three);
		yButtonPressed = OVRInput.Get (OVRInput.Button.Four);

		startButtonPressed = OVRInput.Get (OVRInput.Button.Start);
	}

	//Array List of gun inputs
	//	0 - float - RightTriggerSqueezeAmount
	//  1 - float - LeftTriggerSqueezeAmount
	//	2 - float - RightGripSqueezeAmount
	//  3 - float - LeftGripSqueezeAmount
	//Array List of nav inputs
	//  0 - bool  - rotateLeftButton
	//  1 - bool  - rotateRightButton
	void UpdateGunController(){
		ArrayList gunInputs = new ArrayList ();
		gunInputs.Add (triggerSqueezeAmountRight);
		gunInputs.Add (triggerSqueezeAmountLeft);
		gunInputs.Add (gripSqueezeAmountRight);
		gunInputs.Add (gripSqueezeAmountLeft);
		GC.ReceiveInput (gunInputs);

		ArrayList navInputs = new ArrayList ();
		navInputs.Add (yButtonPressed);
		navInputs.Add (bButtonPressed);
		navInputs.Add (analogStickRight);
		navInputs.Add (analogStickLeft);
		NAV.ReceiveInput (navInputs);
		/**
		ArrayList UIInputs = new ArrayList ();
		UIInputs.Add (xButtonPressed);
		UIInputs.Add (aButtonPressed);
		NAV.ReceiveInput (UIInputs);**/
	}
}
