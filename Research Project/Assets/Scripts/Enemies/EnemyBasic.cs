using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

	public float health = 5f;
	public string weakness;
	public GameObject spawnPart; //particle system that spawns can spawn with this object (not manditory)

	// Use this for initialization

	//AS A NOTE: IF ANY ENEMY SCRIPT USES AWAKE, THIS MAY NOT WORK
	void Awake () {
		//create spawn particle if set
		if (spawnPart != null) {
			GameObject mySParticle = (GameObject)Instantiate (spawnPart, transform);
			mySParticle.transform.localPosition = Vector3.zero;
			Destroy (mySParticle, 2.0f); //destroy the spawn particle system in 2 seconds
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		ShotScript ss = other.gameObject.GetComponent<ShotScript> ();
		if (ss != null) {
			if (other.gameObject.name != "ShardAOE") {
				Destroy (other.gameObject);
			}
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
