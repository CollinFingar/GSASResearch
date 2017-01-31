using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

	public float damage = 1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Gun" && other.tag!= "MainCamera" && other.tag != "Room") {
			print (other.name);
			Destroy (gameObject);
		}

	}
}
