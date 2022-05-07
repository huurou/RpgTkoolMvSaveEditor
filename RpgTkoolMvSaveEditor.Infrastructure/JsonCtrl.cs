using RpgTkoolMvSaveEditor.Domain;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Infrastructure;

public class JsonCtrl : IDataCtrl
{
    private readonly JsonSerializerOptions options_ = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public T? ReadFile<T>(string path)
    {
        return ReadText<T>(File.ReadAllText(path));
    }

    public async Task<T?> ReadFileAsync<T>(string path)
    {
        using var openStream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<T>(openStream, options_);
    }

    public T? ReadText<T>(string text)
    {
        return JsonSerializer.Deserialize<T>(text, options_);
    }

    public async Task<T?> ReadTextAsync<T>(string text)
    {
        var openStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        return await JsonSerializer.DeserializeAsync<T>(openStream, options_);
    }

    public void WriteFile<T>(string path, T model, bool indented = true)
    {
        File.WriteAllText(path, WriteText(model, indented));
    }

    public async Task WriteFileAsync<T>(string path, T model, bool indented = true)
    {
        using var writeStream = File.OpenWrite(path);
        options_.WriteIndented = indented;
        await JsonSerializer.SerializeAsync(writeStream, model, options_);
    }

    public string WriteText<T>(T model, bool indented = true)
    {
        options_.WriteIndented = indented;
        return JsonSerializer.Serialize(model, options_);
    }

    public async Task<string> WriteTextAsync<T>(T model, bool indented = true)
    {
        var writeStream = new MemoryStream();
        options_.WriteIndented = indented;
        await JsonSerializer.SerializeAsync(writeStream, model, options_);
        return Encoding.UTF8.GetString(writeStream.ToArray());
    }
}