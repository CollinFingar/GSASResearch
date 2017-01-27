using UnityEngine;
using System.Collections;

public class EnemyFromImage : MonoBehaviour {
	public Texture2D enemyImage;
	public Color[] pixels;
	public GameObject baseCube;
	// Use this for initialization
	void Start () {
		pixels = enemyImage.GetPixels ();
		GameObject currentSquare;
		for (int i = 0; i < pixels.Length; i++) { //create enemy from pixels
			if (pixels [i].grayscale == 0) {
				Instantiate (baseCube, new Vector3 (this.transform.position.x - 0.25f*8 + (i % 16) * 0.25f, this.transform.position.y - 0.25f*8 + Mathf.Floor (i / 16) * 0.25f, this.transform.position.z), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
