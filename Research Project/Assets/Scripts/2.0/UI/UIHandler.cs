using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (UIEnabled && RaycastTouch) {

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
}
