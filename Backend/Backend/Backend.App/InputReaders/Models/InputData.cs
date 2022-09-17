namespace Backend.App.InputReaders.Models;

public class InputData
{
    public Dictionary<string, InputNodes> Nodes { get; set; }
    public List<List<string>> Connections { get; set; }
}
