using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {

	public LineRenderer rightLine;
	public LineRenderer leftLine;
	public GameObject rightFingerTip;
	public GameObject leftFingerTip;

	public GameObject rightHand;
	public GameObject leftHand;

	public GameObject startButtonObject;
	public Button startButton;

	public GameObject otherButtonObject;
	public Button otherButton;

	public bool usingRight = true;
	private bool currentlyHittingButton = false;
	private Button currentButton;

	public Color buttonHighlightColor = Color.black;
	public Color textHightlightColor = Color.cyan;

	public Button garbageButton;
	public Text startText;
	public Text otherText;

	private float alpha = 0f;
	public SpriteRenderer fader;
	private bool fading = false;

	// Use this for initialization
	void Start () {
		if (usingRight) {
			leftHand.SetActive (false);
		} else {
			rightHand.SetActive (false);
		}
		rightLine.enabled = false;
		leftLine.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		bool aButtonPressed = OVRInput.GetDown (OVRInput.Button.One);
		bool xButtonPressed = OVRInput.GetDown (OVRInput.Button.Three);
		if (aButtonPressed || xButtonPressed) {
			HandleButtons (aButtonPressed, xButtonPressed);
		}
		UpdateRaycast ();
		if (fading) {
			if (alpha >= 1f) {
				FindObjectOfType<AudioHandler> ().LoadMissionMusic ();
				SceneManager.LoadScene (1);
			} else {
				alpha += Time.deltaTime;
				fader.color = new Color (1f, 1f, 1f, alpha);
			}
		}
		SenseMusic ();
	}

	void UpdateRaycast(){
		GameObject fingerTip = rightFingerTip;
		if (!usingRight) {
			fingerTip = leftFingerTip;
		}
		RaycastHit[] hits = Physics.RaycastAll (fingerTip.transform.position, fingerTip.transform.forward, 1000f);
		bool hitStartButton = false;
		bool hitOtherButton = false;
		if (hits.Length > 0) {
			hitStartButton = (hits [0].collider.gameObject == startButtonObject);
			hitOtherButton = (hits [0].collider.gameObject == otherButtonObject);
		}
		if (hitStartButton || hitOtherButton) {
			if (usingRight) {
				if (rightLine.enabled == false) {
					rightLine.enabled = true;
				}
			} else {
				if (leftLine.enabled == false) {
					leftLine.enabled = true;
				}
			}
			currentlyHittingButton = true;
			if (hitStartButton) {
				currentButton = startButton;
				startButton.Select ();
				startText.color = textHightlightColor;
				otherText.color = Color.white;
			} else {
				currentButton = otherButton;
				otherButton.Select ();
				otherText.color = textHightlightColor;
				startText.color = Color.white;
			}
		} else {
			currentlyHittingButton = false;
			rightLine.enabled = false;
			leftLine.enabled = false;
			garbageButton.Select ();
			otherText.color = Color.white;
			startText.color = Color.white;
		}
	}

	void HandleButtons(bool aPressed, bool xPressed){
		if (usingRight && xPressed) {
			usingRight = false;
			rightHand.SetActive (false);
			leftHand.SetActive (true);
		} else if (usingRight && aPressed) {
			//HANDLE BUTTON PRESS
			if(currentlyHittingButton){
				if (currentButton == startButton) {
					StartLoad ();
				}
			}
		}
		if (!usingRight && aPressed) {
			usingRight = true;
			rightHand.SetActive (true);
			leftHand.SetActive (false);
		} else if (!usingRight && xPressed) {
			//HANDLE BUTTON PRESS
			if(currentlyHittingButton){
				if (currentButton == startButton) {
					StartLoad ();
				}
			}
		}
	}

	void StartLoad(){
		fading = true;
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

}
