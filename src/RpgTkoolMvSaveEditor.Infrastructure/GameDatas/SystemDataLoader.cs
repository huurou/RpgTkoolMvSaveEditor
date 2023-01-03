using RpgTkoolMvSaveEditor.Domain;

namespace RpgTkoolMvSaveEditor.Infrastructure.GameDatas;

public class GameDataLoader : IGameDataLoader
{
    private readonly IDataCtrl dataCtrl_;

    public GameDataLoader(IDataCtrl dataCtrl)
    {
        dataCtrl_ = dataCtrl;
    }

    public async Task<TGameData?> LoadAsync<TGameData>(string path)
    {
        return File.Exists(path) ? await dataCtrl_.ReadFileAsync<TGameData>(path) : default;
    }
}