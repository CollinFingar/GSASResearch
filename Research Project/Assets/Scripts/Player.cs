using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public int health = 3;
	public int totalPossibleHealth = 3;

	public SpriteRenderer damageFader;
	private Color damageFaderColor;

	public SpriteRenderer whiteFader;
	private Color whiteFaderColor;

	private float currentAlpha = 0f;
	private float desiredAlpha = 0f;

	private bool faderChanging = false;

	private bool dead = false;
	private bool fadingToWhite = false;
	private float timeToStartFading = 0f;
	private float fadeAlpha = 0f;

	// Use this for initialization
	void Start () {
		health = totalPossibleHealth;
		damageFaderColor = damageFader.color;
		whiteFaderColor = whiteFader.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			if (Time.time > timeToStartFading) {
				fadingToWhite = true;
			}
			if (fadingToWhite) {
				if (fadeAlpha >= 1) {
					SceneManager.LoadScene (SceneManager.GetActiveScene().name);
				} else {
					fadeAlpha += 1 * Time.deltaTime;
					whiteFader.color = new Color (whiteFaderColor.r, whiteFaderColor.g, whiteFaderColor.b, fadeAlpha);
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy Shot") {
			health -= 1;
			Destroy (other.gameObject);
			if (health > 0) {
				desiredAlpha = (1f - (health / (totalPossibleHealth * 1.0f)))*.5f;
			} else {
				desiredAlpha = 1;
				dead = true;
				timeToStartFading = Time.time + 2f;
			}
			damageFader.color = new Color (damageFaderColor.r, damageFaderColor.g, damageFaderColor.b, desiredAlpha);
		}
	}
}
