using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour {

	public float rotateAmount = 90f;

	private bool rotating = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RotateRight(){
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y + rotateAmount, transform.localEulerAngles.z);
	}

	public void RotateLeft(){
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y - rotateAmount, transform.localEulerAngles.z);
	}

	public void ReceiveInput(ArrayList inputs){
		bool rotLeft = (bool)inputs [0];
		bool rotRight = (bool)inputs [1];
		if (rotating) {
			if (!rotLeft && !rotRight) {
				rotating = false;
			}
		} else if(!rotating && rotRight) {
			RotateRight ();
			rotating = true;
		} else if(!rotating && rotLeft) {
			RotateLeft ();
			rotating = true;
		}
	}
}
