using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour {
	public GameObject tilePrefab; //prefab used for floor tile (has node attached)
	public float width;
	public float height;
	public bool updateNodes; //call this when you're editing map and need the nodes to refresh (turn off for finished level prefab)
	public bool createNodes; //call this when you want to create a base map of nodes
	// Use this for initialization
	void Start () {
		if (updateNodes) {
			UpdateNodes ();
		}
		Gizmos.color = Color.white;
		if (createNodes) {
			for (int i = -8; i < 8; i++) {
				for (int j = -8; j < 8; j++) {
					GameObject tmp = (GameObject)Instantiate (tilePrefab, new Vector3 (i * 2, 0, j * 2), Quaternion.identity);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateNodes() { //script that goes to all of this object's children and updates their nodes
		foreach (Transform child in transform) {
			print ("Child");
			FloorNode nodeRef = child.GetComponent<FloorNode>();
			if (nodeRef != null) {
				nodeRef.nodeEdges.Clear ();
				bool upC = checkForNode (nodeRef, 0, height);
				bool rightC = checkForNode (nodeRef, width, 0);
				bool downC = checkForNode (nodeRef, 0, -height);
				bool leftC = checkForNode (nodeRef, -width, 0);
				//check each pair for a diagonal
				if (upC && rightC) {
					checkForNode (nodeRef, width, height);
				}
				if (rightC && downC) {
					checkForNode (nodeRef, width, -height);
				}
				if (downC && leftC) {
					checkForNode (nodeRef, -width, -height);
				}
				if (leftC && upC) {
					checkForNode (nodeRef, -width, height);
				}
				//the following code covers all locations for nodes
			}
		}
	}

	bool checkForNode(FloorNode nodeRef, float x1, float z1) { //check 2 directions and the diagonal for nodes to add
		bool nodeExists = true; //if both directional nodes exist, check for diagonal and add
		if (Physics.CheckSphere (new Vector3 (nodeRef.transform.position.x + x1, nodeRef.transform.position.y, nodeRef.transform.position.z + z1), 0.2f)) {
			Collider[] collisions = Physics.OverlapSphere (new Vector3 (nodeRef.transform.position.x + x1, nodeRef.transform.position.y, nodeRef.transform.position.z + z1), 0.2f);
			bool foundOne = false; //do another check for within this 
			foreach (Collider col in collisions) { //look at each collision
				FloorNode newNode = col.GetComponent<FloorNode> ();
				if (newNode != null) {
					nodeRef.nodeEdges.Add (newNode); //Physical adding of the new edge node
					foundOne = true;
				}
			}
			if (!foundOne) {
				nodeExists = false;
			}
		} else {
			nodeExists = false;
		}
		return nodeExists;
	}

	void OnDrawGizmos() {
		//draw grid of size width x height
		for (float i = -8*height; i < height * 8; i += height) {
			for (float j = -8*width; j < width * 8; j += width) {
				Gizmos.DrawWireCube (new Vector3 (i, 0, j), new Vector3 (height, 0.2f, width));
			}
		}
	}
}
