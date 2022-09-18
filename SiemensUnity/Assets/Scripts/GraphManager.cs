using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

using Backend.Models;

public class GraphManager : MonoBehaviour
{
    public Graph graph = new Graph();
    public string graphString;
    public string fireString;


    private void Start() {
        LoadGraphFromString();
    }

    public void SaveGraph() {
        string convert = JsonConvert.SerializeObject(graph);
        PlayerPrefs.SetString("graph", convert);
        Debug.Log("saved graph: ");
        Debug.Log(convert);
    }

    public void LoadGraph() {
        if (PlayerPrefs.HasKey("graph")) {
            graph = JsonConvert.DeserializeObject<Graph>(PlayerPrefs.GetString("graph"));
        }
    }

    public void LoadGraphFromString() {
        graph = JsonConvert.DeserializeObject<Graph>(graphString);
    }
}
