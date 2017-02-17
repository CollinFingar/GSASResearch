using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
	CheckpointManager checkRef;
	public int spawnCP;
	public float spawnDelay; //delay in seconds until this unit spawns upon reaching the spawning checkpoint
	public GameObject enemyPrefab;
	float spawnTimer = 0;
	bool spawning = false;
	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		gameObject.GetComponent<MeshRenderer> ().enabled = false;
		checkRef.enemyCount [spawnCP]++;
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawning && checkRef.currentCP >= spawnCP) {
			spawning = true;
		}
		//delay to spawn enemies
		if (spawning && spawnTimer < spawnDelay) {
			spawnTimer += Time.deltaTime;
		} else if (spawnTimer > spawnDelay){
			Destroy (this.gameObject);
		}
	}

	void OnDestroy() {
		GameObject spawn = (GameObject)Instantiate (enemyPrefab, this.transform.position,this.transform.rotation);
	}
}
