using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSyncData : NetworkBehaviour {
	public Text currentPlayerText;
	public string otherPlayerText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.isServer) {
			
			//GetComponent<NetworkLobbyPlayer>().player
		}
	
	}

	[Command]
	public void CmdUpdatePlayers() {
		if (currentPlayerText.text == "VR") {
			RpcUpdatePlayers ("PC");
		} else {
			RpcUpdatePlayers ("VR");
		}
	}

	[ClientRpc]
	public void RpcUpdatePlayers(string type){ //call to update text on the non-host's end
		if (!Network.isServer) {
			otherPlayerText = type;
			currentPlayerText.text = type;
		}
	}
}
