using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

	public float health = 5f;
	public string weakness;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		ShotScript ss = other.gameObject.GetComponent<ShotScript> ();
		if (ss != null) {
			Destroy (other.gameObject);
			if (weakness != "" && weakness != ss.type) {
				health -= ss.damage / 2;
			} else if (weakness == ss.type) {
				health -= ss.damage * 2;
			} else {
				health -= ss.damage;
			}
			if (health <= 0f) {
				Destroy (gameObject);
			}
		}
	}
}
