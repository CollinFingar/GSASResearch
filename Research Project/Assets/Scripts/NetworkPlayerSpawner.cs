﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.VR;

public class NetworkPlayerSpawner : NetworkBehaviour {

	public GameObject PCPrefab;
	public GameObject VRPrefab;

	// Use this for initialization
	void Start () {
		GameObject i;
		NetworkConnection conn = this.connectionToClient;
		Destroy (GetComponent<NetworkIdentity> ());
		if (!isLocalPlayer && VRDevice.isPresent) {
			//spawn VR
			i = (GameObject)Instantiate(PCPrefab, transform.position, Quaternion.identity);
			Debug.Log ("Doing NOT local");
		} else if(!isLocalPlayer){
			//spawn PC
			i = (GameObject)Instantiate(VRPrefab, transform.position, Quaternion.identity);
			Debug.Log ("Doing NOT local");
		} else if(isLocalPlayer && VRDevice.isPresent){
			//spawn VR
			i = (GameObject)Instantiate(VRPrefab, transform.position, Quaternion.identity);

			NetworkServer.ReplacePlayerForConnection (conn, i, playerControllerId);
			Debug.Log ("Doing local");
			Debug.Log (playerControllerId);
		} else {
			//spawn PC
			i = (GameObject)Instantiate(PCPrefab, transform.position, Quaternion.identity);
			NetworkServer.ReplacePlayerForConnection (conn, i, playerControllerId);
			Debug.Log ("Doing local");
			Debug.Log (playerControllerId);
		}
		Destroy (gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
