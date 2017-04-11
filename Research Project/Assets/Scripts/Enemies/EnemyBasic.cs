using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

	public float health = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		ShotScript ss = other.gameObject.GetComponent<ShotScript> ();
		if (ss != null) {
			print ("HIT!");
			Destroy (other.gameObject);
			health -= ss.damage;
			if (health <= 0f) {
				Destroy (gameObject);
			}
		}
	}
}
