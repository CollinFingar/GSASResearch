using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Prototype.NetworkLobby
{

public class PlayerSyncData : NetworkBehaviour {
	public Text hostType; //stores host player's type (PC or VR)
	LobbyManager lobbyManagerRef;

	// Use this for initialization
	void Start () {
		lobbyManagerRef = FindObjectOfType<LobbyManager>();
		//typeRef = FindObjectOfType<PlayerType> ();
	}
	
	// Update is called once per frame
	void Update () {
		lobbyManagerRef.mySpawnType = hostType;
		//lobbyManagerRef.mySpawnType = hostType.text;
		//CmdUpdatePlayers ();
	
	}

	[Command]
	public void CmdUpdatePlayers() {
		if (Network.isServer) {
			if (hostType.text == "VR") {
				RpcUpdatePlayers ("PC");
			} else {
				RpcUpdatePlayers ("VR");
			}
		}
	}

	[ClientRpc]
	public void RpcUpdatePlayers(string type){ //call to update text on the non-host's end
		/*
		if (!Network.isServer) {
			typeRef.playerType = type;
		}
		*/

	}
}
}