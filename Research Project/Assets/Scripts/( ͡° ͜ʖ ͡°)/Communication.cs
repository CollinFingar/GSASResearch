using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.VR;

public class Communication : NetworkBehaviour {
	GameObject vrplayer;
	// Use this for initialization
	void Start () {
		vrplayer = null;
		CmdWhatever ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Y)) {
			if (vrplayer == null) {
				vrplayer = FindObjectOfType (typeof(VRGUIHandler));
			}
			CmdChangeEnergy (-5);
		}
	}

	[Command]
	public void CmdWhatever(){
		RpcMeh ();	
	}

	public void CmdChangeEnergy(int amount) {
		RpcChangeEnergy (amount);
	}

	[ClientRpc]
	public void RpcMeh(){
		if (VRSettings.enabled) {
			print ("( ͡° ͜ʖ ͡°)");
		}
	}

	public void RpcChangeEnergy(int amount) {
		if (VRSettings.enabled) {
			vrplayer.GetComponent<VRGUIHandler> ().DecreaseEnergyLevel (amount);
		}
	}
}
