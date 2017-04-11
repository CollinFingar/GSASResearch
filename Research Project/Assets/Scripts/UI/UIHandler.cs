using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIHandler : MonoBehaviour {

	public bool trianglesActive = false;

	public GameObject[] arrows;
	//0 = Front
	//1 = Right
	//2 = Back
	//3 = Left

	public GameObject arrowsFolder;
	private int direction = 0;

	private GameObject[] doors;

	public bool UIEnabled = true;
	public bool RaycastTouch = true;

	public GameObject leftTouch;
	public GameObject rightTouch;

	public GameObject highlightedArrow;

	public GameObject player;
	public GameObject room;

	private bool changingRooms = false;
	private float changeRoomTime = 0f;
	private Vector3 changeRoomLocation;

	public Fader fader;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (UIEnabled && RaycastTouch) {
			RayCastFromControllers ();
		}
		if (Time.time >= changeRoomTime) {
			ChangeRooms ();
		}
	}

	public void UpdateTrianglesDirection(string newDirection, float rotateAmount){
		Vector3 LEA = arrowsFolder.transform.localEulerAngles;
		if (newDirection == "right") {
			arrowsFolder.transform.localEulerAngles = new Vector3 (LEA.x, LEA.y - rotateAmount, LEA.z);
		} else {
			arrowsFolder.transform.localEulerAngles = new Vector3 (LEA.x, LEA.y + rotateAmount, LEA.z);
		}
	}

	public void UpdateTrianglesForRoom(bool[] activeData, GameObject[] doorData){
		doors = doorData;
		for (int i = 0; i < 4; i++) {
			arrows [i].SetActive (activeData [i]);
		}
	}

	public void SetTrianglesState(bool active){
		if (active) {
			trianglesActive = true;
			arrowsFolder.transform.localScale = new Vector3 (1, 1, 1);
		} else {
			trianglesActive = false;
			arrowsFolder.transform.localScale = new Vector3 (0, 0, 0);
		}
		UIEnabled = active;
	}

	public void RayCastFromControllers(){
		RaycastHit[] hits1 = Physics.RaycastAll (rightTouch.transform.position, rightTouch.transform.forward, 2f);
		RaycastHit[] hits2 = Physics.RaycastAll (leftTouch.transform.position, leftTouch.transform.forward, 2f);

		RaycastHit[] hits = hits1.Concat (hits2).ToArray();

		if (hits.Length > 0) {
			
			bool hitAnArrow = false;
			for (int i = 0; i < hits.Length; i++) {
				ArrowScript AS = hits [i].collider.gameObject.GetComponent<ArrowScript> ();
				if (AS != null) {
					hitAnArrow = true;
					if (highlightedArrow != null) {
						if (highlightedArrow != AS.gameObject) {
							highlightedArrow.GetComponent<ArrowScript> ().Highlight (false);
							AS.Highlight (true);
							highlightedArrow = AS.gameObject;
						}
					} else {
						AS.Highlight (true);
						highlightedArrow = AS.gameObject;
					}
				}
			}

			if (!hitAnArrow) {
				if (highlightedArrow != null) {
					highlightedArrow.GetComponent<ArrowScript> ().Highlight (false);
					highlightedArrow = null;
				}
			}

		} else {
			if (highlightedArrow != null) {
				highlightedArrow.GetComponent<ArrowScript> ().Highlight (false);
				highlightedArrow = null;
			}
		}
		Debug.DrawRay (rightTouch.transform.position, rightTouch.transform.forward * 2f, Color.red, .1f);
	}

	public void StartRoomChange(string direction){
		if (!changingRooms) {
			changingRooms = true;
			if (direction == "Front") {
				changeRoomLocation = room.GetComponent<RoomHandler> ().upRoom.transform.position;
			} else if (direction == "Right") {
				changeRoomLocation = room.GetComponent<RoomHandler> ().rightRoom.transform.position;
			} else if (direction == "Back") {
				changeRoomLocation = room.GetComponent<RoomHandler> ().downRoom.transform.position;
			} else {
				changeRoomLocation = room.GetComponent<RoomHandler> ().leftRoom.transform.position;
			}
			changeRoomTime = Time.time + .5f;
			fader.FadeOut (.5f);
		}
	}

	public void ChangeRooms(){
		fader.FadeIn (.5f);
		changingRooms = false;
		player.transform.position = changeRoomLocation;
	}
}
