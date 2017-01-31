using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

	private SpriteRenderer SR;

	private float fadeSpeed = 1f;
	private float alpha = 0f;

	private bool fadingOut = false;
	private bool fadingIn = false;

	// Use this for initialization
	void Start () {
		SR = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fadingOut) {
			alpha += fadeSpeed * Time.deltaTime;
			if (alpha >= 1f) {
				alpha = 1;
				fadingOut = false;
			}
			SR.color = new Color(0f,0f,0f,alpha);
		} else if (fadingIn) {
			alpha -= fadeSpeed * Time.deltaTime;
			if (alpha <= 0) {
				alpha = 0;
				fadingIn = false;
			}
			SR.color = new Color(0f,0f,0f,alpha);
		}
	}

	public void FadeOut(float fadeTime){
		fadeSpeed = 1f / fadeTime;
		fadingOut = true;
		fadingIn = false;
	}
	public void FadeIn(float fadeTime){
		fadeSpeed = 1f / fadeTime;
		fadingIn = true;
		fadingOut = false;
	}
}
