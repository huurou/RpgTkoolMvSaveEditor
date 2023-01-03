namespace RpgTkoolMvSaveEditor.Domain;

public interface IDataCtrl
{
    T? ReadText<T>(string text);

    Task<T?> ReadTextAsync<T>(string text);

    T? ReadFile<T>(string path);

    Task<T?> ReadFileAsync<T>(string path);

    void WriteFile<T>(string path, T model, bool indented = true);

    Task WriteFileAsync<T>(string path, T model, bool indented = true);

    string WriteText<T>(T model, bool indented = true);

    Task<string> WriteTextAsync<T>(T model, bool indented = true);
}