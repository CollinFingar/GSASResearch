using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {

	public Vector3 rotationVector = Vector3.zero;

	public float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 lea = transform.localEulerAngles;
		float x = lea.x + rotationVector.x * speed * Time.deltaTime;
		float y = lea.y + rotationVector.y * speed * Time.deltaTime;
		float z = lea.z + rotationVector.z * speed * Time.deltaTime;
		transform.localEulerAngles = new Vector3 (x, y, z);
	}
}
