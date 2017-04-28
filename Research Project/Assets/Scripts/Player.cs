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

	public float lastTimeHit = 0f;
	public float lastTimeHealed = 0f;
	public float healDelayAfterHit = 5f;
	public float healDelayAfterHeal = .5f;
	private bool healing = false;

	public AudioSource AS;

	// Use this for initialization
	void Start () {
		AS.enabled = false;
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
					SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
				} else {
					fadeAlpha += 1 * Time.deltaTime;
					whiteFader.color = new Color (whiteFaderColor.r, whiteFaderColor.g, whiteFaderColor.b, fadeAlpha);
				}
			}
		} else if (health < totalPossibleHealth && Time.time >= lastTimeHit + healDelayAfterHit) {
			if (healing && Time.time >= lastTimeHealed + healDelayAfterHeal) {
				if (health >= totalPossibleHealth) {
					healing = false;
					health = totalPossibleHealth;
				} else {
					health += 1;
					desiredAlpha = (1f - (health / (totalPossibleHealth * 1.0f)))*.5f;
					lastTimeHealed = Time.time;
				}
			} else if(!healing){
				healing = true;
				health += 1;
				lastTimeHealed = Time.time;
				desiredAlpha = (1f - (health / (totalPossibleHealth * 1.0f)))*.5f;
			}
			damageFader.color = new Color (damageFaderColor.r, damageFaderColor.g, damageFaderColor.b, desiredAlpha);
		}
		SenseMusic ();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy Shot") {
			health -= 1;
			Destroy (other.gameObject);
			healing = false;
			if (health > 0) {
				desiredAlpha = (1f - (health / (totalPossibleHealth * 1.0f))) * .5f;
				lastTimeHit = Time.time;
			} else if(!dead){
				desiredAlpha = 1;
				dead = true;
				PlayDeathNoise ();
				timeToStartFading = Time.time + 2f;
			}
			damageFader.color = new Color (damageFaderColor.r, damageFaderColor.g, damageFaderColor.b, desiredAlpha);
		} else if (other.gameObject.tag == "Enemy") {
			health -= 2;
			Destroy (other.gameObject);
			healing = false;
			if (health > 0) {
				desiredAlpha = (1f - (health / (totalPossibleHealth * 1.0f))) * .5f;
				lastTimeHit = Time.time;
			} else if(!dead){
				desiredAlpha = 1;
				dead = true;
				PlayDeathNoise ();
				timeToStartFading = Time.time + 2f;
			}
			damageFader.color = new Color (damageFaderColor.r, damageFaderColor.g, damageFaderColor.b, desiredAlpha);
		}
	}

	void SenseMusic(){
		AudioHandler handler = null;
		int audioNum = 1;
		if (Input.GetKeyDown (KeyCode.Alpha0)) {
			SceneManager.LoadScene (0);
			audioNum = 0;
			handler = FindObjectOfType<AudioHandler> ();
		} else if(Input.GetKeyDown (KeyCode.Alpha1)) {
			SceneManager.LoadScene (1);
			handler = FindObjectOfType<AudioHandler> ();
		}  else if(Input.GetKeyDown (KeyCode.Alpha2)) {
			SceneManager.LoadScene (2);
			handler = FindObjectOfType<AudioHandler> ();
		}  else if(Input.GetKeyDown (KeyCode.Alpha3)) {
			SceneManager.LoadScene (3);
			handler = FindObjectOfType<AudioHandler> ();
		}  else if(Input.GetKeyDown (KeyCode.Alpha4)) {
			SceneManager.LoadScene (4);
			handler = FindObjectOfType<AudioHandler> ();
		}  else if(Input.GetKeyDown (KeyCode.Alpha5)) {
			SceneManager.LoadScene (5);
			handler = FindObjectOfType<AudioHandler> ();
		}
		if (handler != null) {
			if (audioNum == 1) {
				handler.LoadMissionMusic ();
			} else {
				handler.LoadMenuMusic ();
			}

		}
	}

	void PlayDeathNoise(){
		AS.enabled = true;
		AS.Play ();
	}
}
