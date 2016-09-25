using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.VR;

public class PlayerControllerVR : NetworkBehaviour {

	public GameObject vrCube;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer && !VRSettings.enabled) {
			Destroy (vrCube);
			Debug.Log ("Destoryed");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
