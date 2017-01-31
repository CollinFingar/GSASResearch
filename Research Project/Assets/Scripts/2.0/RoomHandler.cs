using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHandler : MonoBehaviour {

	public bool upDoorActive = false;
	public bool rightDoorActive = false;
	public bool downDoorActive = false;
	public bool leftDoorActive = false;

	public GameObject upDoor;
	public GameObject rightDoor;
	public GameObject downDoor;
	public GameObject leftDoor;

	public GameObject upRoom;
	public GameObject rightRoom;
	public GameObject downRoom;
	public GameObject leftRoom;

	public UIHandler playerUI;

	private bool UIDeactivated = false;
	public int enemyCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerUI != null) {
			if (enemyCount == 0 && UIDeactivated) {
				UIDeactivated = false;
				playerUI.SetTrianglesState (true);
			} else if (enemyCount > 0 && !UIDeactivated) {
				UIDeactivated = true;
				playerUI.SetTrianglesState (false);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "CenterEyeAnchor") {
			playerUI = FindObjectOfType<UIHandler> ();
			if (playerUI != null) {
				bool[] activeData = new bool[]{ upDoorActive, rightDoorActive, downDoorActive, leftDoorActive };
				GameObject[] doorData = new GameObject[]{ upDoor, rightDoor, downDoor, leftDoor };
				playerUI.UpdateTrianglesForRoom (activeData, doorData);
				playerUI.room = gameObject;
			}
		}
	}

	void OnTriggerExit(Collider other){

	}
}
