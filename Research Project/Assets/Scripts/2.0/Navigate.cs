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

	private GameObject nodeAtLocation;

	private LineRenderer LR;

	public GameObject centerEye;

	// Use this for initialization
	void Start () {
		LR = GetComponent<LineRenderer> ();
		LR.enabled = false;
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
		if (!LR.enabled) {
			LR.enabled = true;
		}
		if (!yTeleporting) {
			rayStart = rightTouchController.transform.position;
			rayDirection = rightTouchController.transform.forward;
		} else {
			rayStart = leftTouchController.transform.position;
			rayDirection = leftTouchController.transform.forward;
		}
		rayEnd = rayStart + rayDirection;
		//Debug.DrawLine (rayStart, rayStart + rayDirection * 10, Color.red, .05f);
		RaycastHit[] hits = Physics.RaycastAll (rayStart, rayEnd - rayStart, teleportRayLength);
		if (hits.Length > 0) {
			RaycastHit hit;
			GameObject hitObject = null;
			for (int i = 0; i < hits.Length; i++) {
				if (hits [i].collider.gameObject.tag == "Teleport Node") {
					hit = hits [i];
					hitObject = hits [i].collider.gameObject;
				}
			}
			if (hitObject != null) {
				node = hitObject;
				nodeSelected = true;
				hitObject.GetComponent<NodePoint> ().MakeSelectedMaterial ();
			} else {
				if (node != null) {
					node.GetComponent<NodePoint> ().MakeNormalMaterial ();
				}
				node = null;
				nodeSelected = false;
			}
		} else {
			if (node != null) {
				node.GetComponent<NodePoint> ().MakeNormalMaterial ();
			}
			node = null;
			nodeSelected = false;
		}
		Vector3[] positions = { rayStart, rayStart + rayDirection * 10 };
		if (node != null) {
			positions[1] = node.transform.position;
		}
		LR.SetPositions (positions);
	}

	public void Teleport(){
		transform.position = new Vector3 (node.transform.position.x, 0, node.transform.position.z);
		nodeAtLocation = node;
		node = null;
		GetComponent<CheckpointManager> ().updateNode (nodeAtLocation);
	}

	public void RotateRight(){
		//transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 
		//											transform.localEulerAngles.y + rotateAmount, 
		//											transform.localEulerAngles.z);
		transform.RotateAround(centerEye.transform.position, Vector3.up, rotateAmount);
	}

	public void RotateLeft(){
		//transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 
		//										transform.localEulerAngles.y - rotateAmount, 
		//										transform.localEulerAngles.z);
		transform.RotateAround(centerEye.transform.position, Vector3.up, -1 * rotateAmount);
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
				MakeAllNodesAppear (true);
				teleportMode = true;
				if (yPressed) {
					yTeleporting = true;
				} else {
					yTeleporting = false;
				}
			}
		} else {
			if (teleportMode) {
				MakeAllNodesAppear (false);
				teleportMode = false;
				LR.enabled = false;
			}
			if (node != null) {
				Teleport ();
				node.GetComponent<NodePoint> ().MakeDisappear ();
			}
		}
	}

	public void MakeAllNodesAppear(bool active){
		NodePoint[] nodes = FindObjectsOfType<NodePoint>();
		if (active) {
			if (nodeAtLocation != null) {
				NodePoint nl = nodeAtLocation.GetComponent<NodePoint> ();
				for (int i = 0; i < nodes.Length; i++) {
					if (nodes [i] != nl) {
						nodes [i].MakeNormalMaterial ();
					}
				}
			} else {
				for (int i = 0; i < nodes.Length; i++) {
					nodes [i].MakeNormalMaterial ();
				}
			}
		} else {
			for (int i = 0; i < nodes.Length; i++) {
				nodes [i].MakeDisappear ();
			}
			
		}

	}
}
