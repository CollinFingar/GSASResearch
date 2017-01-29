using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraNoVR : MonoBehaviour {
	private float newX = 0; private float newY = 0; private float newZ = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
