using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour {

	public Sprite[] buttons;
	//0 = A
	//1 = X
	//2 = Y
	//3 = B
	private float alpha = 0f;
	public float alphaRate = .5f;

	private bool displaying = false;
	private Image imageComponent;

	// Use this for initialization
	void Start () {
		imageComponent = GetComponent<Image> ();
		imageComponent.color = new Color (1, 1, 1, alpha);
	}
	
	// Update is called once per frame
	void Update () {
		if (displaying && alpha < 1) {
			alpha += alphaRate * Time.deltaTime;
			imageComponent.color = new Color (1, 1, 1, alpha);
		} else if (!displaying && alpha > 0) {
			alpha -= alphaRate * Time.deltaTime * 1.5f;
			imageComponent.color = new Color (1, 1, 1, alpha);
		}
	}

	public void turnOn(int buttonIndex){
		imageComponent.sprite = buttons [buttonIndex];
		displaying = true;
	}
	public void turnOff(){
		displaying = false;
	}
}
