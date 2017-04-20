using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLRWaveSpawn : MonoBehaviour {
	CheckpointManager checkRef;
	public int spawnCP;
	public float spawnDelay; //delay in seconds until this unit spawns upon reaching the spawning checkpoint
	public int totalSpawns; //total # of enemies to spawn
	public float delayBetwen; //delay between enemy spawns
	public GameObject enemyPrefab;
	public Vector3 enemyMoveDirection; //initial direction enemy should move (includes speed within this vector)
	public float enemyStateTime; //how long to perform this directional movement
	public float enemyPeriodTime; //how far in on the statetime we spawn at (ex: if they are spawning half way through their left or right movement, put 0.5f (1 - #) * enemyStateTime
	public float enemyMovespeed; //how fast the enemy moves towards the player (not other direction dependent)
	float spawnTimer = 0;
	bool spawning = false;
	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		gameObject.GetComponent<MeshRenderer> ().enabled = false;
		checkRef.enemyCount [spawnCP] += totalSpawns;
	}
	
	// Update is called once per frame
	void Update () {
		if (!spawning && checkRef.currentCP >= spawnCP) {
			spawning = true;
		}
		//delay to spawn enemies
		if (spawning && spawnTimer < spawnDelay) {
			spawnTimer += Time.deltaTime;
		} else if (spawnTimer > spawnDelay) {
			spawnDelay = delayBetwen;
			spawnTimer = 0;
			SpawnEnemy ();
			totalSpawns--;
			if (totalSpawns <= 0) {
				Destroy (this.gameObject);
			}
		}
	}

	void SpawnEnemy() {
		GameObject spawn = (GameObject)Instantiate (enemyPrefab, this.transform.position,this.transform.rotation);
		EnemyWaveLR ewlr = spawn.GetComponent<EnemyWaveLR> ();
		ewlr.currentState = enemyStateTime * (1 - enemyPeriodTime);
		ewlr.stateTime = enemyStateTime;
		ewlr.movementDir = enemyMoveDirection;
		ewlr.movespeed = enemyMovespeed;

	}
}
