using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VRGUIHandler : MonoBehaviour {

	public int energyAmountTotal = 15;
	public GameObject energyObject;
	private GameObject[] energyArray;
	
	private int currentEnergyAmount;


	public Color highEnergyColor = new Color(1,0,0,.7f);
	public Color mediumEnergyColor = new Color(1,0,0,.7f);
	public Color lowEnergyColor = new Color(1,0,0,.7f);

	private int colorStateTracker = 2;
	//2 = high
	//1 = medium
	//0 = low

	public int highColorMinimum = 9;
	public int mediumColorMinimum = 5;

	// Use this for initialization
	void Start () {
		InitializeEnergyArray ();
		UpdateEnergyColor (colorStateTracker);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void InitializeEnergyArray(){
		currentEnergyAmount = energyAmountTotal;
		energyArray = new GameObject[energyAmountTotal];
		int e = 0;
		foreach (Transform bar in energyObject.transform) {
			energyArray [e] = bar.gameObject;
			e++;
		}
	}

	void UpdateEnergyLevel(int newAmount){
		if (newAmount > energyAmountTotal || newAmount < 0 || newAmount == currentEnergyAmount) {
			return;
		}
		CheckEnergyColor (newAmount);
		currentEnergyAmount = newAmount;
		for (int e = 0; e < energyAmountTotal; e++) {
			if (e < currentEnergyAmount) {
				energyArray [e].SetActive (true);
			} else {
				energyArray [e].SetActive (false);
			}
		}
	}

	void CheckEnergyColor(int newAmount){
		int newColorState = 2;
		if (newAmount < mediumColorMinimum) {
			newColorState = 0;
		} else if (newAmount < highColorMinimum) {
			newColorState = 1;
		}
		if (newColorState != colorStateTracker) {
			colorStateTracker = newColorState;
			UpdateEnergyColor (colorStateTracker);
		}
	}

	void UpdateEnergyColor(int colorState){
		Color newColor;
		if (colorState == 2) {
			newColor = highEnergyColor;
		} else if (colorState == 1) {
			newColor = mediumEnergyColor;
		} else {
			newColor = lowEnergyColor;
		}
		for (int e = 0; e < energyAmountTotal; e++) {
			energyArray [e].GetComponent<Image> ().color = newColor;
		}
	}
}
