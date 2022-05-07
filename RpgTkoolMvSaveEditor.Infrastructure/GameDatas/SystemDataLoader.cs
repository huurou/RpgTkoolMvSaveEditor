using RpgTkoolMvSaveEditor.Domain;

namespace RpgTkoolMvSaveEditor.Infrastructure.GameDatas;

public class GameDataLoader : IGameDataLoader
{
    private readonly IDataCtrl dataCtrl_;

    public GameDataLoader(IDataCtrl dataCtrl)
    {
        dataCtrl_ = dataCtrl;
    }

    public TGameData? Load<TGameData>(string path)
    {
        return File.Exists(path) ? dataCtrl_.ReadFile<TGameData>(path) : default;
    }
}