using UnityEngine;
using System.Collections;

public struct DoorInfo{
	public int color;
	//0 = white
	//1 = red
	//2 = orange
	//3 = yellow
	//4 = green
	//5 = blue
	//6 = purple
	public int lockType;
	//0 = nolock
	//1 = silverlock
	//2 = goldlock
	public bool closed;
}

public class DoorScript : MonoBehaviour {

	public Material[] lightMaterials;
	public GameObject light;

	public Material[] lockMaterials;
	public GameObject lockObject;

	public DoorInfo DI;

	// Use this for initialization
	void Start () {
		//UpdateLightColor (0);
		//UpdateLockColor (2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(int lightColor, int lockType, bool closed){
		DI.color = lightColor;
		DI.lockType = lockType;
		DI.closed = closed;
		UpdateLightColor (lightColor);
		if (lockType == 0) {
			lockObject.SetActive (false);
		} else {
			UpdateLockColor (lockType);
		}

	}

	public void UpdateLightColor(int color){
		light.GetComponent<Renderer>().material = lightMaterials [color];
	}
	public void UpdateLockColor(int lockType){
		lockObject.GetComponent<Renderer>().material = lockMaterials [lockType-1];
	}
}
