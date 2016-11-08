using UnityEngine;
using System.Collections;

enum flashingState{
	waiting,
	warning,
	active
}

public class ShockFloor : MonoBehaviour {

	public Color restingColor = new Color(0,0,0,1);
	public Color flashColor = new Color (1, 0, 0, .7f);
	public Color shockColor = new Color (1, 0, 0, 1);

	public float waitTime = 1f;
	public float flashTime = 1f;
	public float activeTime = 1f;

	public float flashInterval = .2f;

	private float timeTillSwitch = 0f;
	private float timeTillNextFlash = 0f;
	private bool flashPeak = false;
	private bool needToTurnOn = false;

	private flashingState state;
	private bool oddTiles = false;

	private BoxCollider[] BCs;
	private GameObject[] Tiles;
	private SpriteRenderer[] SRs;

	// Use this for initialization
	void Start () {
		int tileCount = transform.childCount - 1;
		BCs = new BoxCollider[tileCount];
		SRs = new SpriteRenderer[tileCount];
		Tiles = new GameObject[tileCount];
		int t = 0;
		for (int c = 0; c < tileCount + 1; c++) {
			GameObject child = transform.GetChild (c).gameObject;
			if (child.gameObject.name != "Floor") {
				Tiles [t] = child.gameObject;
				SRs [t] = child.gameObject.GetComponent<SpriteRenderer> ();
				SRs [t].color = restingColor;
				BCs [t] = child.gameObject.GetComponent<BoxCollider> ();
				t++;
			}
		}
		state = flashingState.waiting;
		timeTillSwitch = Time.time + waitTime;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateState ();
	}

	void UpdateState(){
		if (state == flashingState.waiting) {
			if (Time.time >= timeTillSwitch) {
				state = flashingState.warning;
				timeTillSwitch = Time.time + flashTime;
				flashPeak = false;
				timeTillNextFlash = Time.time + flashInterval;
				oddTiles = !oddTiles;
			}
		} else if (state == flashingState.warning) {
			if (Time.time >= timeTillSwitch) {
				state = flashingState.active;
				timeTillSwitch = Time.time + activeTime;
				needToTurnOn = true;
			} else if (Time.time >= timeTillNextFlash) {
				if (flashPeak) {
					FlashOn ();
				} else {
					FlashOff ();
				}
				flashPeak = !flashPeak;
				timeTillNextFlash = Time.time + flashInterval;
			}
		} else {
			if (Time.time >= timeTillSwitch) {
				state = flashingState.waiting;
				timeTillSwitch = Time.time + activeTime;
				TurnOff ();
			} else if(needToTurnOn){
				needToTurnOn = false;
				TurnOn ();
			}
		}
	}

	void FlashOn(){
		int startVal = 0;
		if (oddTiles) {
			startVal = 1;
		}
		for (int i = startVal; i < SRs.Length; i += 2) {
			SRs [i].color = flashColor;
		} 
	}

	void FlashOff(){
		int startVal = 0;
		if (oddTiles) {
			startVal = 1;
		}
		for (int i = startVal; i < SRs.Length; i += 2) {
			SRs [i].color = restingColor;
		} 
	}

	void TurnOn(){
		int startVal = 0;
		if (oddTiles) {
			startVal = 1;
		}
		for (int i = startVal; i < SRs.Length; i += 2) {
			SRs [i].color = shockColor;
			Tiles [i].GetComponent<ShockTile> ().shocking = true;
		} 
	}

	void TurnOff(){
		int startVal = 0;
		if (oddTiles) {
			startVal = 1;
		}
		for (int i = startVal; i < SRs.Length; i += 2) {
			SRs [i].color = restingColor;
			Tiles [i].GetComponent<ShockTile> ().shocking = false;
		} 
	}
}
