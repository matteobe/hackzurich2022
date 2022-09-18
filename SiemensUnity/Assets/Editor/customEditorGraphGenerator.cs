using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GraphGenerator))]
public class customEditorGraphGenerator : Editor {

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        GraphGenerator graphGenerator = (GraphGenerator)target;

        if (GUILayout.Button("Create room nodes")) {
            if (Selection.gameObjects.Length == 0) {
                throw new System.Exception("Nothing selected");
            }


        }


        if (GUILayout.Button("Create edge from selection")){
            if (Selection.gameObjects.Length != 2) {
                throw new System.Exception("Too many objects selected");
            }


        }
    }
}


