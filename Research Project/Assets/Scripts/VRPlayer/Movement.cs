using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    //Movment
    public float moveSpeed = 1f;
    public float sprintSpeed = 2f;

    //Rotation
    public float rotationSnap = 90f;
    private bool rotated = false;
    

	void Start () {
	
	}
	
	void Update () {
        HandleRotation();
        HandleMovement();
	}

    void HandleRotation() {
        float value = Input.GetAxis("DHorz");
        if (value != 0 && !rotated){
            transform.Rotate(new Vector3(0, value * rotationSnap, 0));
            rotated = true;
        }
        else if (value == 0 && rotated) {
            rotated = false;
        }
    }

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
