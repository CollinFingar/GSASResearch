using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation1 : EnemyBasic {
	CheckpointManager checkRef;
	public GameObject deathPart;
	public NavMeshAgent meshAgent;
	public GameObject player;

	bool moving = false;
	public float stateTime;
	float comfortDistance; //distance the enemy feels comfortable sitting at and attacking
	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		meshAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		meshAgent.SetDestination (player.transform.position);
		stateTime = Random.Range (2, 4);
		comfortDistance = Random.Range (5, 10);
	}
	
	// Update is called once per frame
	void Update () {
		stateTime -= Time.deltaTime;
		if (stateTime <= 0) {
			if (moving) {
				moving = false;
				meshAgent.SetDestination (transform.position);
				stateTime = Random.Range (2, 4);
			} else {
				float dist = Vector3.Distance (transform.position, player.transform.position);
				//Once player is in sightline, slowly move towards it while attacking
				if (!Physics.Linecast (transform.position, player.transform.position,1 << 10) && dist <= comfortDistance + 1) {
					//slowly move enemy towards player
					if (comfortDistance > 3) {
						comfortDistance -= 0.5f;
					}
					meshAgent.SetDestination (transform.position + transform.right * Random.Range (1f, 2f) * (int)Mathf.Sign(Random.Range(-1,1)) ); //picks a direction and a random distance 1-2 and moves
				} else {
					meshAgent.SetDestination (player.transform.position);
				}
				moving = true;
				stateTime = Random.Range (2, 4);
			}
		}
		transform.LookAt (player.transform.position);
		transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); //remove x component of rotation
		//stop moving when nearing player
	}

	void OnDestroy() {
		checkRef.enemyCount [checkRef.currentCP]--;
		GameObject particle = (GameObject)Instantiate (deathPart, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 2.0f); //destroy particlesystem object after 2 seconds.
	}
}
