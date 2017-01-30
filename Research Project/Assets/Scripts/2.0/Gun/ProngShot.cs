﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProngShot : ShotScript {

	public Vector3 velocity = new Vector3 ();

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + velocity * Time.deltaTime;
	}
}
