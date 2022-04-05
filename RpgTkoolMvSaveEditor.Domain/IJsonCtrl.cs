namespace RpgTkoolMvSaveEditor.Domain;

public interface IJsonCtrl
{
    T? ReadFile<T>(string path);

    T? ReadText<T>(string json);

    void WriteFile<T>(string path, T model, bool indented);

    string WriteText<T>(T model, bool indented);
}