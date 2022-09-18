using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Backend.Models;

public class ColorChanger : MonoBehaviour {
    public GameObject floor;
    public Color color;

    public void ColorAllMapped() {
        GraphManager gm = FindObjectOfType<GraphManager>();
        foreach(KeyValuePair<string, Node> n in gm.graph.nodes) {
            foreach(string floor in n.Value.floorPlanes) {
                GameObject.Find(floor).GetComponent<MeshRenderer>().material.color = color;
            }
        }
    }
}
