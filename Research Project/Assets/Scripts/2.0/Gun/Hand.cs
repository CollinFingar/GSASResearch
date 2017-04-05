using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	//Color Hand for gun selected
	public GameObject[] glowDigits = new GameObject[1];
	public Material gun1DigitMaterial;
	public Material gun2DigitMaterial;

	public Material grabDigitMaterial;

	public bool handInSpawner = false;
	public int spawnerValue = 1;
	public bool rightHand = true;

	//1 = prong, 2 = shotgun
	private int gunSelected = 1;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < glowDigits.Length; i++) {
			glowDigits [i].GetComponent<Renderer> ().material = gun1DigitMaterial;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeGun(){
		if (gunSelected == 1) {
			//Changing to shotgun
			for (int i = 0; i < glowDigits.Length; i++) {
				glowDigits [i].GetComponent<Renderer> ().material = gun2DigitMaterial;
			}
			gunSelected++;
		} else if (gunSelected == 2) {
			//Changing to prong
			for (int i = 0; i < glowDigits.Length; i++) {
				glowDigits [i].GetComponent<Renderer> ().material = gun1DigitMaterial;
			}
			gunSelected--;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Gun Spawner") {
			
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Gun Spawner") {
			if (!handInSpawner) {
				for (int i = 0; i < glowDigits.Length; i++) {
					glowDigits [i].GetComponent<Renderer> ().material = grabDigitMaterial;
				}
				handInSpawner = true;
				spawnerValue = other.gameObject.GetComponent<GunSpawner> ().gunValue;
			}
		} else if (handInSpawner) {
			handInSpawner = false;
			resetDigitColor ();
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Gun Spawner") {
			handInSpawner = false;
			resetDigitColor ();
		}
	}

	public void resetDigitColor(){
		if (gunSelected == 1) {
			for (int i = 0; i < glowDigits.Length; i++) {
				glowDigits [i].GetComponent<Renderer> ().material = gun1DigitMaterial;
			}
		} else if (gunSelected == 2) {
			for (int i = 0; i < glowDigits.Length; i++) {
				glowDigits [i].GetComponent<Renderer> ().material = gun2DigitMaterial;
			}
		}
	}
}
