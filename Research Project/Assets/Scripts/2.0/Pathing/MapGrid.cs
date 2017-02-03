using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour {
	public GameObject tilePrefab; //prefab used for floor tile (has node attached)
	public float width;
	public float height;
	// Use this for initialization
	void Start () {
		Gizmos.color = Color.white;
		for (int i = -8; i < 8; i++) {
			for (int j = -8; j < 8; j++) {
				GameObject tmp = (GameObject)Instantiate (tilePrefab, new Vector3 (i * 2, 0, j * 2), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
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
