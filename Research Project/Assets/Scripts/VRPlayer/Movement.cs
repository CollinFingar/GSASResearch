using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    //MOVEMENT
    public float moveSpeed = 1f;
    public float sprintSpeed = 2f;

    //ROTATION
    //How do you rotate
    public bool ButtonRotation = true;
    public bool StickRotation = true;
    //Rotate speed control
    public float rotationSnap = 90f;
    private bool rotated = false;
    public float stickRotationSnapInterval = .25f;
    private float stickLastSnappedTime = 0f;

	void Start () {
	
	}
	
	void Update () {
        HandleRotation();
        HandleMovement();
	}

    //Gets Gamepad input (Button/Stick) and rotates accordingly
    void HandleRotation() {
        if (ButtonRotation) {
            float value = Input.GetAxis("DHorz");
            if (value != 0 && !rotated){
                transform.Rotate(new Vector3(0, value * rotationSnap, 0));
                rotated = true;
            }
            else if (value == 0 && rotated){
                rotated = false;
            }
        }
        if (StickRotation) {
            float stickValue = Input.GetAxis("RightStickX");
            if (Mathf.Abs(stickValue) > .3f && Time.time > stickLastSnappedTime){
                int val = 1;
                if (stickValue < 0){
                    val = -1;
                }
                transform.Rotate(new Vector3(0, val * rotationSnap, 0));
                stickLastSnappedTime = Time.time + stickRotationSnapInterval;
            }
        }
        if (Input.GetButtonDown("A") || Input.GetButtonDown("Jump")) {
            //GetComponent<Rigidbody>().AddForce(new Vector3(0, 10000, 0));
        }
    }

    //Gets Gamepad input (Stick) and moves accordingly
    void HandleMovement() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool stick = Input.GetButton("LeftStickButton");

        if (x != 0 || z != 0) {
            Vector3 moveVec = new Vector3(x, 0, z);
            float speed = 0;
            if (stick && z > 0) {
                speed = sprintSpeed;
            } else {
                speed = moveSpeed;
            }

            moveVec = transform.TransformDirection(moveVec) * speed * Time.deltaTime * 2;
            transform.position += moveVec;
        }
    }
}
