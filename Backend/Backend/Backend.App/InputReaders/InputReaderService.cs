using System.Text.Json;
using Backend.App.InputReaders.Models;

namespace Backend.App.InputReaders;

public class InputReaderService
{
    public InputData Read()
    {
        var path = Path.Combine(Path.GetDirectoryName(typeof(InputReaderService).Assembly.Location),
            "InputReaders", "data.json");
        using Stream stream = File.OpenRead(path);
        return JsonSerializer.Deserialize<InputData>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
