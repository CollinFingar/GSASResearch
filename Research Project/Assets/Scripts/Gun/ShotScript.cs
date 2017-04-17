using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

	public float damage = 1f;
	public string type; //which type of bullet is this (used with enemy weakness on EnemyBasic script)
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Gun" && other.tag!= "MainCamera" && other.tag != "Room" && other.tag != "Teleport Node") {
			Destroy (gameObject);
		}

	}
}
