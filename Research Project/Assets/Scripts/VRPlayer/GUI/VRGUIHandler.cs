using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VRGUIHandler : MonoBehaviour {

	public int energyPercent = 100;
	public int energyBarsTotal = 10;
	private int energyPerBar;

	public GameObject energyObject;
	private GameObject[] energyArray;
	
	private int currentEnergyBarAmount;
	private int currentEnergyPercent = 100;

	public int displayEnergyPercent = 0;
	private int displayEnergyLagRate = 1;

	public Color highEnergyColor = new Color(1,0,0,.7f);
	public Color mediumEnergyColor = new Color(1,0,0,.7f);
	public Color lowEnergyColor = new Color(1,0,0,.7f);

	private int colorStateTracker = 2;
	//2 = high
	//1 = medium
	//0 = low

	public int highColorMinimum = 9;
	public int mediumColorMinimum = 5;

	public Text energyOnesPlace;
	public Text energyTensPlace;
	public Text energyHundredsPlace;


	// Use this for initialization
	void Start () {
		energyPerBar = energyPercent / energyBarsTotal;
		InitializeEnergyArray ();
		UpdateEnergyColor (colorStateTracker);
		UpdateEnergyText ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateEnergyText ();
	}
		
	void InitializeEnergyArray(){
		currentEnergyBarAmount = energyBarsTotal;
		energyArray = new GameObject[energyBarsTotal];
		int e = 0;
		foreach (Transform bar in energyObject.transform) {
			energyArray [e] = bar.gameObject;
			e++;
		}
	}

	public void DecreaseEnergyLevel(int amount){
		if (amount <= 0 || currentEnergyPercent <= 0) {
			return;
		}
		if (currentEnergyPercent - amount <= 0) {
			currentEnergyPercent = 0;
		} else {
			currentEnergyPercent -= amount;
		}
		int energyBarUpdateAmount = (currentEnergyPercent / energyPerBar) + 1;
		if (currentEnergyPercent == 100) {
			energyBarUpdateAmount = 10;
		} else if (currentEnergyPercent == 0) {
			energyBarUpdateAmount = 0;
		}

		UpdateEnergyLevel (energyBarUpdateAmount);
	}

	void UpdateEnergyLevel(int newAmount){
		if (newAmount > energyBarsTotal || newAmount < 0 || newAmount == currentEnergyBarAmount) {
			return;
		}
		CheckEnergyColor (newAmount);
		currentEnergyBarAmount = newAmount;
		for (int e = 0; e < energyBarsTotal; e++) {
			if (e < currentEnergyBarAmount) {
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
		for (int e = 0; e < energyBarsTotal; e++) {
			energyArray [e].GetComponent<Image> ().color = newColor;
		}
	}

	public int GetCurrentEnergyPercent(){
		return currentEnergyPercent;
	}

	public void UpdateEnergyText(){
		if (displayEnergyPercent != currentEnergyPercent) {
			if (currentEnergyPercent > displayEnergyPercent) {
				if (displayEnergyPercent + displayEnergyLagRate >= currentEnergyPercent) {
					displayEnergyPercent = currentEnergyPercent;
				} else {
					displayEnergyPercent += displayEnergyLagRate;
				}
			} else {
				if (displayEnergyPercent - displayEnergyLagRate <= currentEnergyPercent) {
					displayEnergyPercent = currentEnergyPercent;
				} else {
					displayEnergyPercent -= displayEnergyLagRate;
				}
			}
			int onesPlace = displayEnergyPercent % 10;
			int tensPlace = (displayEnergyPercent / 10)%10;
			int hundredsPlace = 0;
			if (displayEnergyPercent > 99) {
				hundredsPlace = displayEnergyPercent / 100;
			}
			energyOnesPlace.text = onesPlace.ToString ();
			energyTensPlace.text = tensPlace.ToString ();
			energyHundredsPlace.text = hundredsPlace.ToString ();

		}
	}
}
