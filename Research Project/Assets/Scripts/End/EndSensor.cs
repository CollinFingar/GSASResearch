using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSensor : MonoBehaviour {

	public EndBeam eb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.name == "Hand") {
			eb.StartEnd ();
		}
	}
}
