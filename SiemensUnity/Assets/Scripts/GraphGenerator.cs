using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

public enum NodeType {
    Normal,
    EntranceExit,
}

public class Node {
    //public string name;
    public float x, y, z;
}

public class Edge {
    public List<string> edge;
}

public class GraphGenerator : MonoBehaviour
{
    public Dictionary<string,Node> nodes = new Dictionary<string, Node>();
    public List<List<string>> edges = new List<List<string>>();
    public void createGraph() {
        
        foreach(GameObject node in Selection.gameObjects) {
            
            Node n = new Node();
            //n.name = node.name;
            n.x = node.transform.position.x;
            n.y = node.transform.position.y;
            n.z = node.transform.position.z;
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
