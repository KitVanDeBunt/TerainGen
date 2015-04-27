using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(TerainGen))]
public class TerainGenEditor : Editor {

	//private string[] options = new string[] {"Test 1", "Test 2", "Test 3"};
	//private int index = 0;

	public override void OnInspectorGUI () {
		//index = EditorGUILayout.Popup(index, options);
		//EditorGUILayout.LabelField ("Some help", "Some other text");

		if(GUILayout.Button("Generate")) {
			Object[] terains = targets;
			for (int i = 0; i < terains.Length; i++) {
				TerainGen terain = (TerainGen)terains[i];
				terain.Generate();
			}
		}
		//target.speed = EditorGUILayout.Slider ("Speed", target.speed, 0, 100);
		DrawDefaultInspector ();
	}
}

