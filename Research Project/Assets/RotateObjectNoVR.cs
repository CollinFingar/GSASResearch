using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectNoVR : MonoBehaviour {
	private float newX = 0; private float newY = 0; private float newZ = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		newX = 0; newY = 0; newZ = 0;
		if (Input.GetKey (KeyCode.W)) {
			newX += Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)) {
			newX += -Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D)) {
			newY += Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) {
			newY += -Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.Space)) {
			transform.rotation = Quaternion.identity;
		}

		transform.Rotate (Vector3.forward * newY*90);
		transform.Rotate (Vector3.up * newX*90);
	}
}
