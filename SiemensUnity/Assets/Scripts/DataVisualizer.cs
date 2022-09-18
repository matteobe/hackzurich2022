using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

using Backend.Models;

public class FireData {
    public int frequency;
    public List<string> timestamps;
    public Dictionary<string, List<float>> sensors;
}

public class DataVisualizer : MonoBehaviour
{
    public Text timestampText;
    public Slider slider;
    public List<string> sensors = new List<string>() { "f8", "f9", "r19", "f2", "r10", "f7", "r21", "r4", "r8", "r7", "r5", "f13", "r15", "f5", "r16", "f3", "f4", "f6", "f11", "r12", "r2", "r14", "r13", "r17", "r20", "r11", "f12", "r23", "f10", "r1", "f1", "r18", "r9", "r6", "r3", "r22" };

    private bool isPlaying;

    GraphManager gm;
    FireData fd;
    GraphVisualizer gv;


    // Start is called before the first frame update
    void Start() {
        gm = FindObjectOfType<GraphManager>();
        gv = FindObjectOfType<GraphVisualizer>();
        LoadFireData();
        slider.onValueChanged.AddListener(delegate { OnSliderChange(); });
    }

    public void LoadFireData() {
        //TextAsset json = Resources.Load("fire") as TextAsset;
        TextAsset json = Resources.Load("emergency") as TextAsset;
        fd = JsonConvert.DeserializeObject<FireData>(json.text);
        Debug.Log(fd.timestamps.Count);
        slider.maxValue = (int)(fd.timestamps.Count - 1);

        timestampText.text = fd.timestamps[0];
    }

    public void OnSliderChange() {
        timestampText.text = fd.timestamps[(int)slider.value];

        foreach(string sensor in sensors) {
            foreach (string floor in gm.graph.nodes[sensor].floorPlanes) {
                float value = fd.sensors[sensor][(int)slider.value];
                GameObject.Find(floor).GetComponent<MeshRenderer>().material.color = new Color(value / 2000f, 0, 0, 1f);
                
            }
        }
    }

    public void GeneratePath() {
        Node a = gm.graph.nodes["r18"];
        Node b = gm.graph.nodes["r7"];
        List<Node> path = gm.graph.GetShortestPath(a, b);

        for (int i = 1; i < path.Count; i++) {
            gv.VisualizeEdge(path[i - 1], path[i]);
        }
    }

    public void PlayStop() {
        if (isPlaying) {
            StopCoroutine(Play());
            
        } else {
            StartCoroutine(Play());
        }
        isPlaying = !isPlaying;
    }

    IEnumerator Play() {
        while (slider.value < slider.maxValue) { 
            slider.value++;
            yield return new WaitForSeconds(.5f);
        }
    }
}
