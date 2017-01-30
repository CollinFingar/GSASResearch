using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

	public float health = 5f;

	public RoomHandler RH;

	// Use this for initialization
	void Start () {
		RH.enemyCount++;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		ShotScript ss = other.gameObject.GetComponent<ShotScript> ();
		if (ss != null) {
			Destroy (other.gameObject);
			health -= ss.damage;
			if (health <= 0f) {
				Destroy (gameObject);
			}
		}
	}
}
