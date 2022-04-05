using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Domain;

public interface ISaveDataCtrl
{
    void Save(string path, JsonNode jsonNode);

    JsonNode Load(string path);
}