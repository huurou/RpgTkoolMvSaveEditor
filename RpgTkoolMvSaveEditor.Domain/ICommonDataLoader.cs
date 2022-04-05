namespace RpgTkoolMvSaveEditor.Domain;

public interface ICommonDataLoader
{
    CommonData Load(string path);
}