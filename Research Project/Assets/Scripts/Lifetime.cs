using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour {

	public float seconds = 5f;
	private float endLife;

	// Use this for initialization
	void Start () {
		endLife = Time.time + seconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= endLife) {
			Destroy(gameObject);
		}
	}
}
