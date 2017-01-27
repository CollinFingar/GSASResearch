﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
	public GameObject deathPart;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		GameObject particle = (GameObject)Instantiate (deathPart, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 2.0f); //destroy particlesystem object after 2 seconds.
	}
}
