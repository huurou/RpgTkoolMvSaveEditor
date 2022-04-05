using System.Text.Json.Nodes;

namespace RpgTkoolMvSaveEditor.Domain;

public interface ISaveDataSerializer
{
    void Serialize(string path, JsonNode jsonNode);

    JsonNode Deserialize(string path);
}