using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Backend.Models;

public class EditorGraphGenerator : EditorWindow {
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    Graph graph;

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
        GraphVisualizer graphVis = FindObjectOfType<GraphVisualizer>();
        GraphManager graphManager = FindObjectOfType<GraphManager>();

        

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

        if (GUILayout.Button("Vis graph")){
            graphVis.VisualizeGraph();
        }

        if (GUILayout.Button("Load graph")) {
            graphManager.LoadGraph();
            //graphVis.VisualizeGraph();
        }
        if (GUILayout.Button("Load graph from string")) {
            graphManager.LoadGraphFromString();
            //graphVis.VisualizeGraph();
        }

        if (GUILayout.Button("Load fire data")) {
            FindObjectOfType<DataVisualizer>().LoadFireData();
        }

        if(GUILayout.Button("Floor mapping")) {
            string nodeName ="";
            foreach (GameObject obj in Selection.gameObjects) {
                if (graphManager.graph.nodes.ContainsKey(obj.name)) {
                    nodeName = obj.name;
                    break;
                }
            }
            if (nodeName == "") { Debug.Log("no node selected"); return; }
            foreach (GameObject obj in Selection.gameObjects) {
                if (!graphManager.graph.nodes.ContainsKey(obj.name)) {
                    Debug.Log(obj.name);
                    Debug.Log(nodeName);
                    graphManager.graph.nodes[nodeName].floorPlanes.Add(obj.name);
                }
            }
        }

        if (GUILayout.Button("Color all mapped floors")) {
            FindObjectOfType<ColorChanger>().ColorAllMapped();
        }

        if (GUILayout.Button("Start and End Node Selected find shortes path")) {
            Debug.Log("Amount of objects: " + Selection.gameObjects.Length.ToString());
            Node a = graphManager.graph.nodes[Selection.gameObjects[0].name];
            Node b = graphManager.graph.nodes[Selection.gameObjects[1].name];
            List<Node> path = graphManager.graph.GetShortestPath(a, b);

            for(int i = 1; i<path.Count; i++) {
                graphVis.VisualizeEdge(path[i - 1], path[i]);
            }
        }

        if (GUILayout.Button("Delete Arrows")) {
            foreach (ArrowGenerator obj in GameObject.FindObjectsOfType<ArrowGenerator>()) {
                Destroy(obj.gameObject);
            }
        }
    }
}
