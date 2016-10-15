using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI; //Canvas data
using UnityEngine.EventSystems;

public class TerminalText : MonoBehaviour {
	public string[] array_terminal;
	string text_display;
	public InputField input_terminal; // reference to the input field used for writing to this termianl
	// Use this for initialization
	void Start () {
		array_terminal = new string[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void inputFieldSubmit(Text messageHold) {
		if (Input.GetKeyDown (KeyCode.Return)) { //only activate if the return key is pressed (prevents click off activation, silly Unity...)
			pushTerminal (messageHold);
			input_terminal.text = ""; //clear text out of terminal input

			input_terminal.ActivateInputField ();
		}
	}

		
	public void pushTerminal (Text messageHold) {
		string message = messageHold.text;
		if (array_terminal.Length == 20) {
			string temp = array_terminal [0];
			string temp2 = array_terminal [1];
			for (int i = 0; i < 19; i++) {
				array_terminal [i + 1] = temp;
				temp = temp2;
				if (i < 18) {
					temp2 = array_terminal [i + 2];
				}
			}
		} else {
			string[] newArray = new string[array_terminal.Length + 1];
			for (int i = 0; i < array_terminal.Length; i++) {
				newArray [i + 1] = array_terminal [i];
			}
			array_terminal = newArray;
		}
		array_terminal [0] = message; //add message to front of array
		text_display = ""; //empty text_display
		for (int i = array_terminal.Length - 1; i >= 0; i--) { //top to bottom terminal display
			text_display += array_terminal [i] + "\n";
		}
		gameObject.GetComponent<Text> ().text = text_display;
		return;
	}
}
