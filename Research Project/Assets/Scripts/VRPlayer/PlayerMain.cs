using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMain : NetworkBehaviour {

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

	private bool ableToAccessDoor = false;
	private GameObject nearbyDoor = null;

	private float invincibleTime = 2f;
	private float timeTillInvincibleEnds = 0f;

	//GUI
	public VRGUIHandler gui;
	public ButtonPrompt BP;

	void Start () {
		if (!isLocalPlayer) {
			transform.GetChild (0).gameObject.SetActive (false);
		}
	}
	
	void Update () {
		if (isLocalPlayer) {
			HandleRotation();
			HandleMovement();
		}
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
        if (Input.GetButtonDown("X") || Input.GetButtonDown("X")) {
			if (nearbyDoor != null && ableToAccessDoor) {
				GameObject partnerDoor = nearbyDoor.GetComponent<DoorScript> ().DI.partner;
				transform.position = partnerDoor.transform.position + partnerDoor.transform.forward * 1.5f;
				transform.localEulerAngles = new Vector3 (0, partnerDoor.transform.localEulerAngles.y, 0);
			}
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

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Door") {
			//print ("Near door!");
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Door") {
			//print ("Bye door!");
			if (ableToAccessDoor) {
				BP.turnOff ();
				ableToAccessDoor = false;
			}

		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Door") {
			triggerStayDoor (other);
		} else if (other.gameObject.tag == "Shock Tile") {
			if (other.gameObject.GetComponent<ShockTile> ().shocking && Time.time >= timeTillInvincibleEnds) {
				gui.DecreaseEnergyLevel (20);
				timeTillInvincibleEnds = Time.time + invincibleTime;
			}
		}
	}



	void triggerStayDoor(Collider other){
		Vector3 doorRot = other.gameObject.transform.forward;
		Vector3 myRot = transform.forward * -1;
		float angle = Vector3.Angle (myRot, doorRot);
		if (Mathf.Abs (angle) <= 50 && !ableToAccessDoor) {
			//print ("Looking at door!");
			if (!other.gameObject.GetComponent<DoorScript> ().DI.closed) {
				ableToAccessDoor = true;
				BP.turnOn (1);
				nearbyDoor = other.gameObject;
				//print ("Can go through door!");
			} else {
				ableToAccessDoor = false;
				//print ("Can't go through door...");
			}
		} else if (Mathf.Abs (angle) > 50) {
			if (ableToAccessDoor) {
				BP.turnOff ();
				ableToAccessDoor = false;
			}
		}
	}

}
