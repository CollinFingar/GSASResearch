using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public Camera cameraS; //scene camera
	public Camera cameraT; //terminal camera
	public Camera cameraD; //level details camera
	// Use this for initialization
	void Start () {
		cameraS.depth = 0;
		cameraT.depth = 1;
		cameraD.depth = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("tab")) {
			if (cameraS.depth == 2) {
				cameraS.depth = 0;
			} else {
				cameraS.depth++;
			}
			if (cameraT.depth == 2) {
				cameraT.depth = 0;
			} else {
				cameraT.depth++;
			}
			if (cameraD.depth == 2) {
				cameraD.depth = 0;
			} else {
				cameraD.depth++;
			}
		}
	}
}
