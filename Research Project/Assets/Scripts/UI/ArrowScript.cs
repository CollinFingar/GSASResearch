using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour {

	public Color defaultColor = new Color();
	public Color highlightColor = new Color ();

	public UIHandler UI;

	public string Direction = "Front";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Highlight(bool active){
		if (active) {
			transform.GetChild (1).gameObject.GetComponent<Image> ().color = highlightColor;
		} else {
			transform.GetChild (1).gameObject.GetComponent<Image> ().color = defaultColor;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player Shot") {
			UI.StartRoomChange (Direction);
			//UI.ChangeRooms (Direction);
		}
	}
}
