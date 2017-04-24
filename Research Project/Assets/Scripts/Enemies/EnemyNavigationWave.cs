using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigationWave : EnemyBasic {
	CheckpointManager checkRef;
	public GameObject deathPart;
	public NavMeshAgent meshAgent;
	public GameObject player;

	//FLOATING "ANIMATION"
	bool floatUp = true; //model slowly floats up and down
	float floatTime = 1; //time to float up or down
	public GameObject modelRef; //reference to the child of this gameobject which holds are "frames" of the enemy 

	public List<Transform> pathToFollow;
	int pathPoint = 0; //current point on the path this enemy is on
	public float distanceThreshold; //distance before point switches

	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		meshAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player").transform.GetChild(0).GetChild(1).gameObject;
		meshAgent.SetDestination (pathToFollow[pathPoint].position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform.position);
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); //remove x component of rotation
		//Time for Moving Back and Forth
		if (Vector3.Distance (transform.position, pathToFollow [pathPoint].position) <= distanceThreshold) {
			pathPoint++;
		}

		if (pathPoint >= pathToFollow.Count) {
			meshAgent.SetDestination (player.transform.position);
		} else {
			meshAgent.SetDestination (pathToFollow [pathPoint].position);
		}

		//Floating Up and Down
		floatTime -= Time.deltaTime;
		if (floatUp) {
			if (floatTime <= 0) {
				floatUp = false;
				floatTime = 2;
			}
			modelRef.transform.position += Vector3.up * 0.05f * Time.deltaTime;
		} else {
			if (floatTime <= 0) {
				floatUp = true;
				floatTime = 2;
			}
			modelRef.transform.position -= Vector3.up * 0.05f * Time.deltaTime;
		}
	}

	void OnDestroy() {
		checkRef.enemyCount [checkRef.currentCP]--;
		GameObject particle = (GameObject)Instantiate (deathPart, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 2.0f); //destroy particlesystem object after 2 seconds.
	}
}
