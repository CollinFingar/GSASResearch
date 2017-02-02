using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShot : ShotScript {

	public Vector3 velocity = new Vector3 ();
	public GameObject blasterParticle;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2f);
	}

	// Update is called once per frame
	void Update () {
		transform.position = transform.position + velocity * Time.deltaTime;
	}

	void OnDestroy() {
		GameObject particle = (GameObject)Instantiate (blasterParticle, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z),Quaternion.identity);
		Destroy (particle, 1.0f); //destroy particlesystem object after 2 seconds.
	}
}
