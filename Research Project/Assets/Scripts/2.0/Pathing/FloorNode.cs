using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorNode : MonoBehaviour {
	public List<GameObject> nodeEdges;
	// Use this for initialization
	void Start () {
		nodeEdges = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		//nodeEdges.Clear ();
		
	}

	void OnCollisionEnter(Collision col) {
		nodeEdges.Add (col.gameObject);
	}
}
