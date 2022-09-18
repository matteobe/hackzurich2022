using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.Models;

public class GraphVisualizer : MonoBehaviour
{

    public GameObject arrowPrefab;
    public GraphManager graphManager;

    public void VisualizeGraph() {
        foreach(KeyValuePair<string, Node> n in graphManager.graph.nodes) {
            foreach(KeyValuePair<string, double> edge in n.Value.edges) {
                ArrowGenerator arrowGen = Instantiate<GameObject>(arrowPrefab).GetComponent<ArrowGenerator>();
                arrowGen.GenerateArrow(n.Value, graphManager.graph.nodes[edge.Key]);
            }
        }
    }

    public void VisualizeEdge(Node a, Node b) {
        ArrowGenerator arrowGen = Instantiate<GameObject>(arrowPrefab).GetComponent<ArrowGenerator>();
        arrowGen.GenerateArrow(a, b);
    }
}
