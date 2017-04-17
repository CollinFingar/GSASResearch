using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : EnemyBasic {
	CheckpointManager checkRef;
	public GameObject deathPart;
	public GameObject player;

	//FLOATING "ANIMATION"
	bool floatUp = true; //model slowly floats up and down
	float floatTime = 1; //time to float up or down
	public GameObject modelRef; //reference to the child of this gameobject which holds are "frames" of the enemy 
	public Vector3 myRotation;

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
		player = GameObject.Find ("Player");
		stateTime = Random.Range (2, 4);
		comfortDistance = Random.Range (5, 10);
	}

	// Update is called once per frame
	void Update () {
		if (stateTime <= 0) {
			if (!Physics.Linecast (transform.position, player.transform.position, 1 << 10)) {
				//Attack Check
				if (!attacking && cooldown <= 0) {
					AttackStart ();
				}
				moving = true;
				stateTime = Random.Range (2, 4);
			}
		} else {
			stateTime -= Time.deltaTime;
		}
		transform.Rotate (myRotation * Time.deltaTime);

		//stop moving when nearing player
		if (cooldown > 0) {
			cooldown -= Time.deltaTime;
			if (cooldown <= 0 && attacking) {
				AttackStop ();
			}
		}

		//Floating Up and Down
		floatTime -= Time.deltaTime;
		if (floatUp) {
			if (floatTime <= 0) {
				floatUp = false;
				floatTime = 2;
			}
			modelRef.transform.position += Vector3.up * 0.03f * Time.deltaTime;
		} else {
			if (floatTime <= 0) {
				floatUp = true;
				floatTime = 2;
			}
			modelRef.transform.position -= Vector3.up * 0.03f * Time.deltaTime;
		}
	}

	void OnDestroy() {
		checkRef.enemyCount [checkRef.currentCP]--;
		GameObject particle = (GameObject)Instantiate (deathPart, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 2.0f); //destroy particlesystem object after 2 seconds.
	}

	void AttackStart() {
		attacking = true;
		cooldown = 1;
		//Projecile Launch
		if (projectile != null) {
			GameObject attackProj = (GameObject)Instantiate (projectile, transform.position, transform.rotation);
		}
	}

	void AttackStop() {
		attacking = false;
		cooldown = attackCooldown + Random.Range (attackCDMod, -attackCDMod);
	}
}
