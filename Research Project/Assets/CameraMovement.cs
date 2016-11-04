using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Canvas data
public class CameraMovement : MonoBehaviour {
	public Camera cameraS; //scene camera
	public Camera cameraT; //terminal camera
	public Camera cameraD; //level details camera

	public InputField input_terminal;
	// Use this for initialization
	void Start () {
		cameraS.depth = 1;
		cameraT.depth = 0;
		cameraD.depth = -1;
		cameraD.rect = new Rect (0, 0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("tab")) {
			if (cameraS.depth == 1) {
				cameraS.depth = 0;
				//cameraS.rect = new Rect (0, 0, 0.5f, 1);
			} else {
				cameraS.depth++;
				cameraS.rect = new Rect (0, 0, 1, 1);
				input_terminal.DeactivateInputField (); // terminal is being removed from screen, so deactivate it as well
			}
			if (cameraT.depth == 1) {
				cameraT.depth = 0;
				cameraT.rect = new Rect (1,1,0,0); //removes terminal from screen
			} else {
				cameraT.rect = new Rect (0.5f, 0.15f, 0.5f, 0.8f);
				input_terminal.ActivateInputField ();	
				cameraT.depth++;
			}
		}
		if (cameraS.depth > cameraT.depth) {
			if (Input.GetKeyDown ("1")) {
				print ("Numbah 1");
			} else if (Input.GetKeyDown ("2")) {
				print ("Numbah 2");
			} else if (Input.GetKeyDown ("3")) {
				print ("Numbah 3");
			}
		}
	}
}
