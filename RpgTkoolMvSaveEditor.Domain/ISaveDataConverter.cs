using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Domain;

public interface ISaveDataConverter
{
    void FronJsonNode(string path, JsonNode jsonNode);
    JsonNode ToJsonNode(string path);
}