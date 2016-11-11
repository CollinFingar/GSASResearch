﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.VR;

public class Communication : NetworkBehaviour {
	VRGUIHandler vrplayer;
	// Use this for initialization
	void Start () {
		vrplayer = null;
		CmdWhatever ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Y)) {
			CmdChangeEnergy (5);
		}
	}

	[Command]
	public void CmdWhatever(){
		RpcMeh ();	
	}
	[Command]
	public void CmdChangeEnergy(int amount) {
		RpcChangeEnergy (amount);
	}

	[ClientRpc]
	public void RpcMeh(){
		if (VRSettings.enabled) {
			print ("( ͡° ͜ʖ ͡°)");
		}
	}
	[ClientRpc]
	public void RpcChangeEnergy(int amount) {
		if (vrplayer == null) {
			vrplayer = (VRGUIHandler)FindObjectOfType (typeof(VRGUIHandler));
		}
		if (VRSettings.enabled) {
			print ("yeah boi");
			vrplayer.GetComponent<VRGUIHandler> ().DecreaseEnergyLevel (amount);
		}
	}
}
