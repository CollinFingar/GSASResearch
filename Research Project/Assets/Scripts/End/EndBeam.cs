using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBeam : MonoBehaviour {

	public ParticleSystem badSystem;
	public ParticleSystem goodSystem;

	public Material crystalGood;

	public GameObject crystal;

	public SpriteRenderer baseGlow;

	private bool touched = false;
	private float alpha = 0f;

	public float delayTime = 4f;
	private float endTime = 0f;

	private SpriteRenderer playerFader;


	// Use this for initialization
	void Start () {
		goodSystem.gameObject.SetActive (false);
		playerFader = GameObject.Find ("Fader").GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			StartEnd ();
		}
		if (touched && Time.time > endTime) {
			if (alpha >= 1f) {
				//END LEVEL
			} else {
				alpha += 1 * Time.deltaTime;
				playerFader.color = new Color(1f,1f,1f,alpha);
			}
		}
	}

	public void StartEnd(){
		if (!touched) {
			touched = true;
			badSystem.Stop ();
			goodSystem.gameObject.SetActive (true);
			crystal.GetComponent<Renderer> ().material = crystalGood;
			baseGlow.color = Color.white;
			endTime = Time.time + delayTime;
		}
	}
}
