using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.VR;

public class NetworkPlayerSpawner : NetworkBehaviour {

	public GameObject PCPrefab;
	public GameObject VRPrefab;

	// Use this for initialization
	void Start () {
		GameObject i;
		if (!isLocalPlayer && VRDevice.isPresent) {
			//spawn VR
			i = (GameObject)Instantiate(PCPrefab, transform.position, Quaternion.identity);
		} else if(!isLocalPlayer){
			//spawn PC
			i = (GameObject)Instantiate(VRPrefab, transform.position, Quaternion.identity);
		} else if(isLocalPlayer && VRDevice.isPresent){
			//spawn VR
			i = (GameObject)Instantiate(VRPrefab, transform.position, Quaternion.identity);
			NetworkConnection conn = this.connectionToClient;
			NetworkServer.ReplacePlayerForConnection (conn, i, playerControllerId);
		} else {
			//spawn PC
			i = (GameObject)Instantiate(PCPrefab, transform.position, Quaternion.identity);
			NetworkConnection conn = this.connectionToClient;
			NetworkServer.ReplacePlayerForConnection (conn, i, playerControllerId);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
