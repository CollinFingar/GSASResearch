using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraNoVR : MonoBehaviour {
	private float newX = 0; private float newY = 0; private float newZ = 0;
	ArrayList newControls;
	public GunController controlRef;
	// Use this for initialization
	void Start () {
		newControls = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controlRef != null) {
			newControls.Clear ();
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				newControls.Add (1f);
			} else {
				newControls.Add (0f);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				newControls.Add (1f);
			} else {
				newControls.Add (0f);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				newControls.Add (1f);
			} else {
				newControls.Add (0f);
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				newControls.Add (1f);
			} else {
				newControls.Add (0f);
			}
			controlRef.ReceiveInput (newControls);
		}
		newX = 0; newY = 0; newZ = 0;
		if (Input.GetKey (KeyCode.UpArrow)) {
			newY = Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			newY = -Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			newX = Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			newX = -Time.deltaTime;
		}
		transform.position = new Vector3(transform.position.x + newX, transform.position.y + newY, transform.position.z + newZ);
	}


}
