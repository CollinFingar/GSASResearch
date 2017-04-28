using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile1 : MonoBehaviour {
	public GameObject explosionParticle;
	Vector3 randomRotation;
	Vector3 target;
	public float speed;
	// Use this for initialization
	void Start () {
		randomRotation = new Vector3 (Random.Range (10, 20f), Random.Range (10, 20f), Random.Range (10, 20f));
		target = GameObject.Find ("Player").transform.GetChild (0).GetChild (1).position - this.transform.position;
		float maxComp = Mathf.Max (Mathf.Abs(target.x), Mathf.Abs(target.y), Mathf.Abs(target.z));
		target = new Vector3 (target.x / maxComp, target.y / maxComp, target.z / maxComp);
	}
	
	// Update is called once per frame
	void Update () {
		//Randomly rotate and move towards the player's location when this spawned
		transform.Rotate (randomRotation * Time.deltaTime);
		transform.position += target* Time.deltaTime*speed;
	}
	void OnDestroy() {
		GameObject particle = (GameObject)Instantiate (explosionParticle, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 1.0f); //destroy particlesystem object after a second.
	}

	void OnTriggerEnter(Collider other){
		if (other.tag != "Gun" && other.tag != "Enemy" && other.tag!= "MainCamera" && other.tag != "Room" && other.tag != "Teleport Node") {
			Destroy (gameObject);
		}

	}

}
