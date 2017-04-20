using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaveLR : EnemyBasic {
	CheckpointManager checkRef;
	public GameObject deathPart;
	public NavMeshAgent meshAgent;
	public GameObject player;

	//FLOATING "ANIMATION"
	bool floatUp = true; //model slowly floats up and down
	float floatTime = 1; //time to float up or down
	public GameObject modelRef; //reference to the child of this gameobject which holds are "frames" of the enemy 

	//AI Specific Variables (Moving Back and Forth)
	public float stateTime;
	public float currentState;
	public Vector3 movementDir; //direction this enemy is moving in (reverses when stateTime expires)
	public float movespeed;

	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		meshAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player").transform.GetChild(0).GetChild(1).gameObject;
		meshAgent.SetDestination (player.transform.position);
		currentState = stateTime;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform.position);
		transform.rotation = Quaternion.Euler(-90, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z); //remove x component of rotation
		//Time for Moving Back and Forth
		if (currentState <= 0) {
			currentState = stateTime;
			movementDir *= -1; //reverse movement direction
		} else {
			currentState -= Time.deltaTime;
			transform.position += movementDir * Time.deltaTime;
			transform.position += -transform.right * movespeed * Time.deltaTime;
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
