using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathLight : MonoBehaviour {
	public Light myLight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		myLight.intensity -= Time.deltaTime;
	}
}
