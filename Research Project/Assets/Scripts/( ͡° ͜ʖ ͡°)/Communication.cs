using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.VR;

public class Communication : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		CmdWhatever ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Command]
	public void CmdWhatever(){
		RpcMeh ();	
	}

	[ClientRpc]
	public void RpcMeh(){
		if (isLocalPlayer && VRSettings.enabled) {
			print ("( ͡° ͜ʖ ͡°)");
		}
	}
}
