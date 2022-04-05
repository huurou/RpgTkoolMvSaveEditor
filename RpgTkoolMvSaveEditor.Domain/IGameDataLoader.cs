namespace RpgTkoolMvSaveEditor.Domain;

public interface IGameDataLoader
{
    TGameData? Load<TGameData>(string path) where TGameData : GameData;
}
