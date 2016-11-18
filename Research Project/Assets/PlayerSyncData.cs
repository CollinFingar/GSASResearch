using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncData : NetworkBehaviour {
	[SyncVar]
	public string otherPlayerText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
