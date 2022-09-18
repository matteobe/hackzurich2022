using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Backend.Models;

public class GraphGenerator : MonoBehaviour
{
    public Dictionary<string,Node> nodes = new Dictionary<string, Node>();
    public List<List<string>> edges = new List<List<string>>();

    public GraphManager graphManager;

    private void Start() {
        
    }

    public void createGraph() {
        
        foreach(GameObject node in Selection.gameObjects) {
            
            Node n = new Node(node.transform.position.x, node.transform.position.y, node.transform.position.z, NodeType.Normal, node.name);
            graphManager.graph.nodes.Add(node.name, n);
        }
    }

    public void createEdge(GameObject a, GameObject b) {
        Node nodeA = graphManager.graph.nodes[a.name];
        Node nodeB = graphManager.graph.nodes[b.name];

        nodeA.AddEdge(nodeB);
        nodeB.AddEdge(nodeA);
    }

    public void printGraph() {
        graphManager.SaveGraph();
    }
}
