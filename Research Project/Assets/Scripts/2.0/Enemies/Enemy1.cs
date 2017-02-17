using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBasic {
	CheckpointManager checkRef;
	public GameObject deathPart;

	// Use this for initialization

	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
	}
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnDestroy() {
		checkRef.enemyCount [checkRef.currentCP]--;
		GameObject particle = (GameObject)Instantiate (deathPart, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 2.0f); //destroy particlesystem object after 2 seconds.
	}
}
