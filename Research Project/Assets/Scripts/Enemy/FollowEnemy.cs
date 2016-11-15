using UnityEngine;
using System.Collections;

public class FollowEnemy : BasicEnemy {

	public float speed = 1f;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("VRPlayer(Clone)");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.Find ("VRPlayer(Clone)");
		} else if(Vector3.Distance(player.transform.position, transform.position) < 15f){
			Vector3 playVec = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
			transform.rotation = Quaternion.LookRotation (playVec - transform.position);
			float step = speed * Time.deltaTime;
			transform.position =  transform.position + (transform.forward * step);
		}

	}
}
