using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class SenseVR : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (VRDevice.isPresent) {
			Debug.Log ("yup");
		} else {
			Debug.Log ("nope");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
