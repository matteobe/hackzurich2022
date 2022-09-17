using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Backend.Models;

public enum NodeType {
    Normal,
    EntranceExit,
}

public class GraphGenerator : MonoBehaviour
{
    public Dictionary<string,Node> nodes = new Dictionary<string, Node>();
    public List<List<string>> edges = new List<List<string>>();
    public void createGraph() {
        
        foreach(GameObject node in Selection.gameObjects) {
            
            Node n = new Node(node.transform.position.x, node.transform.position.y, node.transform.position.z, Backend.Models.NodeType.Normal);
            nodes.Add(node.name, n);
        }

        string output = JsonConvert.SerializeObject(nodes);

        //Debug.Log(output);
    }

    public void createEdge(GameObject a, GameObject b) {
        Node nodeA = nodes[a.name];
        Node nodeB = nodes[b.name];

        List<string> edge = new List<string>();
        edge.Add(a.name);
        edge.Add(b.name);

        edges.Add(edge);
    }

    public void printGraph() {
        string graph = "{ \"Nodes\": ["+ JsonConvert.SerializeObject(nodes) + "], \"Connections\": " + JsonConvert.SerializeObject(edges) + "}";
        Debug.Log(graph);
    }
}
