using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGrid))]
public class MapBuilder : Editor {
	
	MapGrid grid;
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable() {
		grid = (MapGrid)target;
	}
	/*
	public override void OnInspectorGUI() {
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Grid Width");
		grid.width = EditorGUILayout.Slider (grid.width, 1f, 100f, null);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Grid Height");
		grid.height = EditorGUILayout.Slider (grid.height, 1f, 100f, null);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Edit Tiles")) {

		}
		GUILayout.EndHorizontal ();
		 
		grid.tilePrefab = (GameObject)EditorGUILayout.ObjectField ("Tile Prefab",grid.tilePrefab, typeof(GameObject), null);
	}

	void OnSceneGUI() {
		int controlid = GUIUtility.GetControlID (FocusType.Passive);
		Event e = Event.current;
		RaycastHit hit;
		Vector3 hitpos; //position ray hits, where placement is based from
		Ray ray = Camera.current.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray,out hit,100f)) {
			hitpos = hit.point;
			if (e.isMouse && e.type == EventType.MouseDown && e.button == 0) { //if left mouse button is pressed
				GUIUtility.hotControl = controlid;
				e.Use ();

				GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab (grid.tilePrefab);
				tmp.transform.position = hitpos;
				tmp.transform.parent = grid.gameObject.transform;
			}
		}
	}
	*/
}
