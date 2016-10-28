using UnityEngine;
using System.Collections;
using System.IO;

public class RoomManager : MonoBehaviour {

	public GameObject roomPrefab;

	public GameObject[] roomArray;

	public string folderLocation = "Assets/Resources/LevelFiles/";
	public string fileName = "1-1.txt";

	private string levelName = "";
	private int numRoomsDeep = 0;
	private int numRoomsWide = 0;

	// Use this for initialization
	void Start () {
		ReadFile ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReadFile (){
		StreamReader reader = new StreamReader (folderLocation + fileName);
		ArrayList roomLines = new ArrayList ();

		string text = " ";
		text = reader.ReadLine ();
		int lineNum = 0;

		while(text != null){
			if (lineNum == 0) {
				levelName = text;
			} else if (lineNum == 1) {
				string[] vals = text.Split (',');
				numRoomsWide = int.Parse (vals [0]);
				numRoomsDeep = int.Parse (vals [1]);
			} else {
				roomLines.Add (text);
			}
			text = reader.ReadLine ();
			lineNum++;
		}
		print (	"Level: " + levelName + "\n" + 
			numRoomsWide.ToString() + " rooms wide\n" + 
			numRoomsDeep.ToString() + " rooms deep\n");
		CreateRooms (roomLines);
	}

	void CreateRooms(ArrayList roomLines){
		roomArray = new GameObject[roomLines.Count];
		for (int i = 0; i < roomLines.Count; i++) {

			string line = (string)roomLines [i];
			string[] lineArr = line.Split (',');
			string[] posStringArr = lineArr [0].Split ('-');
			int width = int.Parse (posStringArr [1]);
			int depth = int.Parse (posStringArr [0]);

			Vector3 startPos = new Vector3 (width * 30, 0, depth * -30);

			GameObject temp = (GameObject)Instantiate (roomPrefab, startPos, Quaternion.identity);
			temp.name = lineArr [0];
			int roomType = int.Parse (lineArr [1]);
			int floorType = int.Parse (lineArr [2]);
			string[] doors = new string[4];
			doors [0] = lineArr [3];
			doors [1] = lineArr [4];
			doors [2] = lineArr [5];
			doors [3] = lineArr [6];
			temp.GetComponent<RoomScript> ().Initialize (roomType, doors, floorType);
			temp.transform.parent = transform;
			roomArray [i] = temp;
		}
	}
}
