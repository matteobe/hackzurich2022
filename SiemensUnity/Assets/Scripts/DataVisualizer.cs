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

    GraphManager gm;
    FireData fd;
    // Start is called before the first frame update
    void Start() {
        gm = FindObjectOfType<GraphManager>();
        LoadFireData();
        slider.onValueChanged.AddListener(delegate { OnSliderChange(); });
    }

    public void LoadFireData() {
        TextAsset json = Resources.Load("fire") as TextAsset;
        fd = JsonConvert.DeserializeObject<FireData>(json.text);
        Debug.Log(fd.sensors.Values.Count);

        timestampText.text = fd.timestamps[0];
    }

    public void OnSliderChange() {
        timestampText.text = fd.timestamps[(int)slider.value];

        foreach(KeyValuePair<string, List<float>fd.sensors)
    }
}
