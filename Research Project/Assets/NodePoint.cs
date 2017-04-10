using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePoint : MonoBehaviour {
	CheckpointManager checkRef;
	public int nodeNum; //which number this checkpoint is in the level (used in triggering progression)
	public int spawnCP; //starting checkpoint when it first spawns
	public int maxCP; //maximum checkpoint that can be achieved at this node
	public int endCP; //end checkpoint when it de-spawns 

	public Material[] materials = new Material[2];
	//0=not selected
	//1=selected

	// Use this for initialization
	void Start () {
		checkRef = FindObjectOfType<CheckpointManager> ();
		MakeDisappear ();
	}
	
	// Update is called once per frame
	void Update () {
		//prevent player from returning to this point once he leaves
		if (checkRef.currentCP >= endCP) {
			Destroy (gameObject); 
		}
		Vector3 ea = transform.localEulerAngles;
		transform.localEulerAngles = new Vector3 (ea.x, ea.y, ea.z + 25 * Time.deltaTime);
	}
	public void MakeSelectedMaterial(){
		GetComponent<Renderer> ().enabled = true;
		GetComponent<Renderer> ().material = materials [1];
	}

	public void MakeNormalMaterial(){
		if (checkRef.currentCP >= spawnCP) { //prevent points from being attainable early
			GetComponent<Renderer> ().enabled = true;
			GetComponent<Renderer> ().material = materials [0];	
		}
	}

	public void MakeDisappear(){
		GetComponent<Renderer> ().enabled = false;
	}
}
