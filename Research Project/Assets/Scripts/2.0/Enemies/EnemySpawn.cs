using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	CheckpointManager checkRef;
	public int spawnCP;
	public float spawnDelay; //delay in seconds until this unit spawns upon reaching the spawning checkpoint
	bool spawning = false;
	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		//gameObject.GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawning && checkRef.currentCP >= spawnCP) {

		}
	}

	void OnDestroy() {
		checkRef.enemyCount [checkRef.currentCP]--; //subtract this enemy from the enemies remaining
	}
}
