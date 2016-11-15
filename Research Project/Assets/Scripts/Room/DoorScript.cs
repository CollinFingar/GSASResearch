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

	public GameObject partner;
}

public class DoorScript : MonoBehaviour {

	public Material[] lightMaterials;
	public GameObject light;
	public GameObject PC_light;

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
		PC_light.GetComponent<Renderer> ().material = lightMaterials [color];
	}
	public void UpdateLockColor(int lockType){
		lockObject.GetComponent<Renderer>().material = lockMaterials [lockType-1];
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Door") {
			print (other.transform.parent.transform.parent.name);
			DoorScript otherDoorScript = other.gameObject.GetComponent<DoorScript> ();
			if (otherDoorScript.DI.lockType == 0 && DI.lockType == 0) {
				if (otherDoorScript.DI.color == DI.color) {
					DI.partner = otherDoorScript.gameObject;
					DI.closed = false;
				} else {
					DI.partner = null;
					DI.closed = true;
				}
			} else if (otherDoorScript.DI.lockType == DI.lockType) {
				DI.partner = otherDoorScript.gameObject;
				DI.closed = false;
			} else {
				DI.partner = null;
				DI.closed = true;
			}
			if (DI.partner != null) {
				print ("PARTNERED");
			}
		}
	}
}
