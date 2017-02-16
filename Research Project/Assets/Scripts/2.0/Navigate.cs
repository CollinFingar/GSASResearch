using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour {

	//public int directionIndex = 0;
	//public string[] directions = new string[]{"Up", "Right", "Down", "Left"};

	public float rotateAmount = 90f;
	private bool rotating = false;

	private float timeTillNextRotate = 0f;
	public float rotateInterval = .4f;

	public UIHandler UI;

	public GameObject leftTouchController;
	public GameObject rightTouchController;

	private bool teleportMode = false;
	private bool yTeleporting = false;
	private bool nodeSelected = false;

	private GameObject node;
	public float teleportRayLength = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (teleportMode) {
			UpdateTeleportSensing ();
		}
	}

	public void UpdateTeleportSensing(){
		Vector3 rayStart;
		Vector3 rayDirection;
		Vector3 rayEnd;
		if (!yTeleporting) {
			rayStart = rightTouchController.transform.position;
			rayDirection = rightTouchController.transform.forward;
		} else {
			rayStart = leftTouchController.transform.position;
			rayDirection = leftTouchController.transform.forward;
		}
		rayEnd = rayStart + rayDirection;
		Debug.DrawLine (rayStart, rayStart + rayDirection * 10, Color.red, .05f);
		RaycastHit[] hits = Physics.RaycastAll (rayStart, rayEnd - rayStart, teleportRayLength);
		if (hits.Length > 0) {
			RaycastHit hit = hits [0];
			if (hit.collider.gameObject.tag == "Teleport Node") {
				node = hit.collider.gameObject;
				nodeSelected = true;
			} else {
				node = null;
				nodeSelected = false;
			}
		}
	}

	public void Teleport(){
		transform.position = new Vector3 (node.transform.position.x, 0, node.transform.position.z);
	}

	public void RotateRight(){
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 
													transform.localEulerAngles.y + rotateAmount, 
													transform.localEulerAngles.z);
	}

	public void RotateLeft(){
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 
												transform.localEulerAngles.y - rotateAmount, 
												transform.localEulerAngles.z);
	}
		
	public void ReceiveInput(ArrayList inputs){
		Vector2 rightAnalogVector = (Vector2)inputs [2];
		float rightAnalogX = rightAnalogVector.x;
		Vector2 leftAnalogVector = (Vector2)inputs [3];
		float leftAnalogY = leftAnalogVector.x;
		float valueTogether = rightAnalogX + leftAnalogY;
		bool enoughToRotate = Mathf.Abs (valueTogether) > .4;
		if (enoughToRotate && Time.time >= timeTillNextRotate) {
			if (valueTogether > 0) {
				//ROTATE RIGHT
				RotateRight();
			} else {
				//ROTATE LEFT
				RotateLeft();
			}
			timeTillNextRotate = Time.time + rotateInterval;
		}
		
		bool yPressed = (bool)inputs [0];
		bool bPressed = (bool)inputs [1];
		if (yPressed || bPressed) {
			if (teleportMode) {
				if (yTeleporting && bPressed && !yPressed) {
					yTeleporting = false;
				} else if (!yTeleporting && !bPressed && yPressed) {
					yTeleporting = true;
				}

			} else {
				teleportMode = true;
				if (yPressed) {
					yTeleporting = true;
				} else {
					yTeleporting = false;
				}
			}
		} else {
			teleportMode = false;
			if (node != null) {
				Teleport ();
			}
		}
	}
}
