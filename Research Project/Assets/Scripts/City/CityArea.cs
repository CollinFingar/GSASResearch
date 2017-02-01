using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityArea : MonoBehaviour {
	public Material colorLocked;
	public Material colorUnlocked;
	public Material baseLit; //material for base when selected
	public Material baseUnlit; //material for base when not selected
	List<GameObject> buildingEdges;
	List<GameObject> buildingBases;
	// Use this for initialization
	void Start () {
		//acquire data for the city on runtime (in case changes are made at any point)
		buildingEdges = new List<GameObject> ();
		buildingBases = new List<GameObject> ();
		foreach (Transform sub in transform) {
			if (sub.gameObject.name == "Edges") {
				foreach (Transform subsub in sub) {
					buildingEdges.Add (subsub.gameObject);
				}
			} else if (sub.gameObject.name == "Bases") {
				foreach (Transform subsub in sub) {
					buildingBases.Add (subsub.gameObject);
				}
			}
		}

		//TESTING
		LockArea();

		//END TESTING
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			LockArea();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			UnlockArea ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			SelectArea ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			DeselectArea ();
		}
	}

	//Script to highlight area when the cursor is over it
	void UnlockArea () {
		for (int i = 0; i < buildingEdges.Count; i++) {
			buildingEdges [i].GetComponent<Renderer>().material = colorUnlocked;
		}
	}

	void LockArea () {
		for (int i = 0; i < buildingEdges.Count; i++) {
			buildingEdges [i].GetComponent<Renderer> ().material = colorLocked;
		}
	}
	
	void SelectArea () {
		for (int i = 0; i < buildingBases.Count; i++) {
			buildingBases [i].GetComponent<Renderer> ().material = baseLit;
		}
	}

	void DeselectArea() {
		for (int i = 0; i < buildingBases.Count; i++) {
			buildingBases [i].GetComponent<Renderer> ().material = baseUnlit;
		}
	}
}
