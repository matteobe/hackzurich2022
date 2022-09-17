using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorGraphGenerator : EditorWindow {
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("GraphGenerator/Generate")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        EditorGraphGenerator window = (EditorGraphGenerator)EditorWindow.GetWindow(typeof(EditorGraphGenerator));
        window.Show();
    }

    void OnGUI() {
        GUILayout.Label("GenerateGraph Tool", EditorStyles.boldLabel);
        /*myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();*/

        GraphGenerator graphGenerator = FindObjectOfType<GraphGenerator>();

        if (GUILayout.Button("Create room nodes")) {
            if (Selection.gameObjects.Length == 0) {
                throw new System.Exception("Nothing selected");
            }

            graphGenerator.createGraph();
        }


        if (GUILayout.Button("Create edge from selection")) {
            if (Selection.gameObjects.Length != 2) {
                throw new System.Exception("Too many objects selected");
            }

            graphGenerator.createEdge(Selection.gameObjects[0], Selection.gameObjects[1]);
        }

        if (GUILayout.Button("Print graph")) {
            graphGenerator.printGraph();
        }

    }
}
