using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float moveSpeed = 1f;
    public float rotationSnap = 90f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("A")) {
            
        }
        if (Input.GetAxis("RightTrigger") != 0) {
            Debug.Log(Input.GetAxis("RightTrigger"));
        }
	}
}
