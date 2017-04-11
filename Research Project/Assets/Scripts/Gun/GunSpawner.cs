using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour {

	public int gunValue = 1;
	//1 = openBlaster
	//2 = ???
	//3 = ???

	public bool depleted = false;
	public float respawnDelay = 10f;
	private float timeTillNextSpawn = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
