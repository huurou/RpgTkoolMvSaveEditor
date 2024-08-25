using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Queries;
using RpgTkoolMvSaveEditor.Model.Queries.Common;
using RpgTkoolMvSaveEditor.Util.Results;

namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService(Context context, IQueryHandler<GetSaveDataQuery, SaveDataViewDto> getSaveDataQueryHandler)
{
    public void SelectWwwDir(string path)
    {
        context.WwwDirPath = path;
    }

    public async Task<Result<SaveDataViewDto>> GetSaveDataAsync(GetSaveDataQuery query)
    {
        return await getSaveDataQueryHandler.HandleAsync(query);
    }
}

public class Context
{
    // 引数でファイルパスを渡すとインターフェースの汎用性が下がるのでコンストラクタで渡すようにした
    public string? WwwDirPath { get; set; }
}
