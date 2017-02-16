using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePoint : MonoBehaviour {
	CheckpointManager checkRef;
	public int nodeNum; //which number this checkpoint is in the level (used in triggering progression)
	public int spawnCP; //starting checkpoint when it first spawns
	public int maxCP; //maximum checkpoint that can be achieved at this node
	public int endCP; //end checkpoint when it de-spawns 
	bool active;
	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!active && checkRef.currentCP >= spawnCP) {
			activatePoint ();
		} else if (checkRef.currentCP >= endCP) {
			deactivatePoint ();
		}
	}
	//when the player teleports to this node
	void activatePoint () {
		active = true;
		GetComponent<MeshRenderer> ().enabled = false;
	}

	void deactivatePoint () {
		active = false;
		GetComponent<MeshRenderer> ().enabled = true;
	}

}
