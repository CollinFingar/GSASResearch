using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {

	public float health = 2f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Yep");
		if (other.tag == "Player Shot") {
			health -= .5f;
			if (health <= 0) {
				Destroy (gameObject);
			}
			Destroy (other.gameObject);
		}
	}
}
