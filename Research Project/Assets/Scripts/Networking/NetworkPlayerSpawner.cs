using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.VR;

public class NetworkPlayerSpawner : NetworkBehaviour {

	public GameObject PCPrefab;
	public GameObject VRPrefab;

	// Use this for initialization
	void Start () {
		/**
		Debug.Log (playerControllerId);
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
		} else {
			//spawn PC
			i = (GameObject)Instantiate(PCPrefab, transform.position, Quaternion.identity);
			NetworkServer.ReplacePlayerForConnection (conn, i, playerControllerId);
			Debug.Log ("Doing local");
		}
		Destroy (gameObject);
		**/
		if (isLocalPlayer) {
			GameObject i;
			NetworkConnection conn = this.connectionToClient;
			if (VRDevice.isPresent) {
				i = (GameObject)Instantiate(VRPrefab, transform.position, Quaternion.identity);
				NetworkServer.Spawn (VRPrefab);
				NetworkServer.ReplacePlayerForConnection (conn, i, playerControllerId);
			} else {
				i = (GameObject)Instantiate(PCPrefab, transform.position, Quaternion.identity);
				NetworkServer.Spawn (PCPrefab);
				NetworkServer.ReplacePlayerForConnection (conn, i, playerControllerId);
			}
			NetworkServer.Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
