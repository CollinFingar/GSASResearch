using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ShotScript {

	public Vector3 velocity = new Vector3 ();
	public GameObject particle;
	public GameObject aoe;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3f);
	}

	// Update is called once per frame
	void Update () {
		transform.position = transform.position + velocity * Time.deltaTime;
	}

	void OnDestroy() {
		ShootShards();
	}

	void ShootShards(){
		GameObject p = (GameObject)Instantiate (particle, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		GameObject a = (GameObject)Instantiate (aoe, this.transform.position, Quaternion.identity);
		Destroy (a, 1f);
		Destroy (p, 1.0f); //destroy particlesystem object after 2 seconds.
	}
}
