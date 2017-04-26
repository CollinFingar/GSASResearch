using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : EnemyBasic {
	CheckpointManager checkRef;
	public GameObject deathPart;
	Vector3 colliderCenter; 
	Vector3 colliderSize;
	bool moving = true;

	//the following two variables handle small "animation" for enemy
	int floatUp = 1;
	float floatTimer = 0.75f;

	float delayTime = 2;
	float timer = 2;
	int dirRoll;
	// Use this for initialization

	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		colliderSize = GetComponent<BoxCollider> ().size;
		colliderSize = new Vector3 (colliderSize.x * transform.localScale.x, colliderSize.y * transform.localScale.y, colliderSize.z * transform.localScale.z);
		colliderCenter = GetComponent<BoxCollider> ().center;
		dirRoll = Random.Range (0, 4);
	}
	// Update is called once per frame
	void Update () {
		transform.LookAt (checkRef.transform);	
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, transform.eulerAngles.z);
		timer -= Time.deltaTime;
		if (moving) {
			transform.position += getDirection (dirRoll) * Time.deltaTime;
			if (timer <= 0) {
				moving = false;
				timer = Random.Range (2, 4);
			}
		} else if (timer <= 0) {
			moving = true;
			dirRoll = Random.Range (0, 4);
			timer = Random.Range (2, 4);
		}
		//Floating
		transform.position += 0.25f*Time.deltaTime*transform.up * (1 - 2 * floatUp);
		floatTimer -= Time.deltaTime;
		if (floatTimer <= 0) {
			floatUp = 1 - floatUp;
			floatTimer = 0.75f;
		}
	}
		
	void OnDestroy() {
		checkRef.enemyCount [checkRef.currentCP]--;
		GameObject particle = (GameObject)Instantiate (deathPart, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 2.0f); //destroy particlesystem object after 2 seconds.
	
	}
		
	void OnTriggerStay(Collider collision) {
		if (collision.gameObject.tag != "Teleport Node" && collision.gameObject.tag != "Enemy") {
			print ("collision");
			transform.position -= getDirection (dirRoll) * Time.deltaTime;
			dirRoll = Random.Range (0, 4); //CURRENTLY DOESN'T GO UP OR DOWN
		}
	}

	Vector3 getDirection(int roll) {
		switch (roll) {
		case 0:
			return transform.forward;
		case 1:
			return transform.forward * -1; //back
		case 2:
			return transform.right;
		case 3:
			return transform.right * -1; //left
		case 4:
			return transform.up;
		case 5:
			return transform.up * -1;
		}
		return Vector3.zero;
	}

	//check for a collision in a given direction
	bool checkCollision(Vector3 direction) {
		Vector3 checkCenter = new Vector3 (transform.position.x + (colliderSize.x / 2 + Time.deltaTime) * direction.x,
			                      transform.position.y + (colliderSize.y / 2 + Time.deltaTime) * direction.y,
			                      transform.position.z + (colliderSize.z / 2 + Time.deltaTime) * direction.z);
		new Vector3 (colliderSize.x * (1 - Mathf.Abs (direction.x)),
			colliderSize.y * (1 - Mathf.Abs (direction.y)),
			colliderSize.z * (1 - Mathf.Abs (direction.z)));
		print (checkCenter);
		print (colliderSize);
		return Physics.CheckBox (checkCenter, Vector3.zero,transform.rotation);
		/*
			new Vector3(colliderSize.x * (1 - Mathf.Abs(direction.x)),
				colliderSize.y * (1 - Mathf.Abs(direction.y)),
				colliderSize.z * (1 - Mathf.Abs(direction.z)) ),transform.rotation);
		*/
	}
}
