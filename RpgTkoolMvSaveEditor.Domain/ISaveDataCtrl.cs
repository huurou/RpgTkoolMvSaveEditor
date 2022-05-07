using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Domain;

public interface ISaveDataCtrl
{
    Task SaveAsync(string path, JsonNode jsonNode);

    Task<JsonNode> LoadAsync(string path);
}