using RpgTkoolMvSaveEditor.Domain;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Infrastructure;

public class JsonCtrl : IJsonCtrl
{
    public T? ReadFile<T>(string path)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        return JsonSerializer.Deserialize<T>(File.ReadAllText(path), options);
    }

    public T? ReadText<T>(string json)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        return JsonSerializer.Deserialize<T>(json, options);
    }

    public void WriteFile<T>(string path, T model, bool indented)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = indented,
        };
        File.WriteAllText(path, JsonSerializer.Serialize(model, options));
    }

    public string WriteText<T>(T model, bool indented)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = indented,
        };
        return JsonSerializer.Serialize(model, options);
    }
}