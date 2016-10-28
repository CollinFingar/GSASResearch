using UnityEngine;
using System.Collections;

public class RoomScript : MonoBehaviour {

	public GameObject doorPrefab;
	public GameObject[] floorPrefabs;
	public GameObject wallPrefab;

	public GameObject[] doors;
	//0 = Top
	//1 = Right
	//2 = Bottom
	//3 = Left
	public GameObject doorFolder;

	public float radius = 12.5f;


	// Use this for initialization
	void Start () {
		string[] testArray = new string[4];
		testArray [0] = "w";
		testArray [1] = "r";
		testArray [2] = "b";
		testArray [3] = "gl";

		//Initialize (1, testArray, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(int roomType, string[] doorSetUp, int floor){
		//roomType- 0=Beginning, 1=Middle, 2=End
		//doorSetUp- 'n'=no door, 'w'=color, 'r'=red, 'o'=orange, 'y'=yellow,
		//			'g'=green, 'b'=blue, 'p'=purple, 'sl'=silver lock, 'gl'=gold lock
		//floor- different floor styles
		GameObject wallInstance = (GameObject)Instantiate(wallPrefab, transform.position, transform.rotation);
		wallInstance.transform.parent = transform;
		InitializeDoors (doorSetUp);
		GameObject floorInstance = (GameObject)Instantiate(floorPrefabs[floor], transform.position, transform.rotation);
		floorInstance.transform.parent = transform;
	}

	void InitializeDoors(string[] doorSetUp){
		int total = 0;
		for(int i = 0; i < doorSetUp.Length; i++){
			if (doorSetUp [i] != "n") {
				total += 1;
			}
		}
		doors = new GameObject[total];
		for(int i = 0; i < doorSetUp.Length; i++){
			if (doorSetUp [i] != "n") {
				Vector3 position = new Vector3(0,0,0);
				Vector3 localAngles = new Vector3(0,180,0);
				//Top
				if (i == 0) {
					position = transform.TransformPoint(new Vector3(0,0,12.5f));
					localAngles = new Vector3(0,180,0);
					//Right
				} else if (i == 1) {
					position = transform.TransformPoint(new Vector3(12.5f,0,0));
					localAngles = new Vector3(0,270,0);
					//Bottom
				} else if (i == 2) {
					position = transform.TransformPoint(new Vector3(0,0,-12.5f));
					localAngles = new Vector3(0,0,0);
					//Left
				} else {
					position = transform.TransformPoint(new Vector3(-12.5f,0,0));
					localAngles = new Vector3(0,90,0);
				}

				int lightColor = 0;
				int lockType = 0;
				if (doorSetUp [i] == "w") {
				
				} else if (doorSetUp [i] == "r") {
					lightColor = 1;
				} else if (doorSetUp [i] == "o") {
					lightColor = 2;
				} else if (doorSetUp [i] == "y") {
					lightColor = 3;
				} else if (doorSetUp [i] == "g") {
					lightColor = 4;
				} else if (doorSetUp [i] == "b") {
					lightColor = 5;
				} else if (doorSetUp [i] == "p") {
					lightColor = 6;
				} else if (doorSetUp [i] == "sl") {
					lockType = 1;
				} else if (doorSetUp [i] == "gl") {
					lockType = 2;
				}
				GameObject doorInstance = (GameObject)Instantiate (doorPrefab, position, Quaternion.Euler (localAngles));
				doorInstance.transform.parent = doorFolder.transform;
				doorInstance.GetComponent<DoorScript> ().Initialize (lightColor, lockType, true);
			}
		}
	}
}
