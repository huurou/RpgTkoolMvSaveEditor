using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Domain;

public interface ISaveDataCtrl
{
    void Save(string path, JsonNode jsonNode);

    Task<JsonNode> LoadAsync(string path);
}