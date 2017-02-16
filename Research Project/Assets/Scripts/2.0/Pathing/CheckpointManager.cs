using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour {
	public int currentCP; //current checkpoint the player is on (has various conditions depending on the number)
	public int[] enemyCount; //number of enemies at each checkpoint, created on run time by enemies adding themselves to this array

	public ArrayList checkTriggers;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
