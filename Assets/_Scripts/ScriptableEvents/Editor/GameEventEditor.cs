using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor {


	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		GUILayout.Space(20);
		GUI.enabled = Application.isPlaying;

		GameEvent e = target as GameEvent;
		if (GUILayout.Button("Raise")) {
			e.Raise();
		}
	}
}
