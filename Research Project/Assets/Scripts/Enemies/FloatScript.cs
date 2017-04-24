using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FloatScript : EnemyBasic {
	CheckpointManager checkRef;
	public GameObject deathPart;
	public NavMeshAgent meshAgent;
	public GameObject player;
	public Renderer[] myFrames; //this enemies frames of animation (mostly 2)

	//FLOATING "ANIMATION"
	bool floatUp = true; //model slowly floats up and down
	public float floatTime = 1; //time to float up or down
	public GameObject modelRef; //reference to the child of this gameobject which holds are "frames" of the enemy 

	bool moving = false;
	public float stateTime;

	//THIS AI SPECIFIC VARIABLES (Ranged AI)
	float comfortDistance; //distance the enemy feels comfortable sitting at and attacking
	public GameObject projectile; //prefab of the projectile this enemy will attack with
	bool attacking = false; //whether or not this object is in attack animation (also determines the use of cooldown)
	float cooldown = 0f; //cooldown used for attacking as well as resetting attack animations

	public float attackCooldown; //how long between attacks
	public float attackCDMod; //modifier range for time between attacks (0 is constant time, goes positive and negative otherwise)
	public GameObject attackOffset; //empty object that serves as an offset of projectile spawn location (set near mouth)


	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		meshAgent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Floating Up and Down
		floatTime -= Time.deltaTime;
		if (floatUp) {
			if (floatTime <= 0) {
				floatUp = false;
				floatTime = 2;
			}
			transform.position += Vector3.up * 0.05f * Time.deltaTime;
		} else {
			if (floatTime <= 0) {
				floatUp = true;
				floatTime = 2;
			}
			transform.position -= Vector3.up * 0.05f * Time.deltaTime;
		}
	}

	void OnDestroy() {
		checkRef.enemyCount [checkRef.currentCP]--;
		GameObject particle = (GameObject)Instantiate (deathPart, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 2.0f); //destroy particlesystem object after 2 seconds.
	}

	void AttackStart() {
		myFrames [0].enabled = false;
		myFrames [1].enabled = true;
		attacking = true;
		cooldown = 1;
		//Projecile Launch
		if (projectile != null) {
			GameObject attackProj = (GameObject)Instantiate (projectile, attackOffset.transform.position, attackOffset.transform.rotation);
		}
	}

	void AttackStop() {
		myFrames [0].enabled = true;
		myFrames [1].enabled = false;
		attacking = false;
		cooldown = attackCooldown + Random.Range (attackCDMod, -attackCDMod);
	}
}
