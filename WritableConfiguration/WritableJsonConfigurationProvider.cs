using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration.Json;

namespace WritableOptions;

public class WritableJsonConfigurationProvider(JsonConfigurationSource source) : JsonConfigurationProvider(source)
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public override void Set(string key, string? value)
    {
        base.Set(key, value);
        const string emptyJson = "{}";
        var fileFullPath = Source.FileProvider!.GetFileInfo(Source.Path!).PhysicalPath;
        var json = File.Exists(fileFullPath) ? File.ReadAllText(fileFullPath) : emptyJson;
        var jsonObj = JsonSerializer.Deserialize<JsonObject>(json);
        jsonObj![key] = JsonNode.Parse(value ?? emptyJson);
        var output = JsonSerializer.Serialize(jsonObj, Options);
        File.WriteAllText(fileFullPath!, output);
    }
}