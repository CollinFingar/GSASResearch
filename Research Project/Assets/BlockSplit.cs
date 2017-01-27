using UnityEngine;
using System.Collections;

public class BlockSplit : MonoBehaviour {
	bool dead = false;
	// Use this for initialization
	void Start () {
		dead = true;
		GameObject.Destroy (this.gameObject, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			deathSplit ();
		}
	}

	void deathSplit() { //called once per frame when dying

	}
}
