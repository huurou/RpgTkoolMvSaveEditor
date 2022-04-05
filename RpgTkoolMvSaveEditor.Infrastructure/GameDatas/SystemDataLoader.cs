using RpgTkoolMvSaveEditor.Domain;
using RpgTkoolMvSaveEditor.Domain.GameDatas;

namespace RpgTkoolMvSaveEditor.Infrastructure.GameDatas;

public class GameDataLoader : IGameDataLoader
{
    private readonly IJsonCtrl jsonCtrl_;

    public GameDataLoader(IJsonCtrl jsonCtrl)
    {
        jsonCtrl_ = jsonCtrl;
    }

    public TGameData? Load<TGameData>(string path) where TGameData : GameData
    {
        return jsonCtrl_.ReadFile<TGameData>(path);
    }
}