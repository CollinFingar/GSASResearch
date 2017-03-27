using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	//Color Hand for gun selected
	public GameObject[] glowDigits = new GameObject[1];
	public Material gun1DigitMaterial;
	public Material gun2DigitMaterial;

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
}
