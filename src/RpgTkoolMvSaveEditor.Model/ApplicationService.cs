using RpgTkoolMvSaveEditor.Model.Commands;
using RpgTkoolMvSaveEditor.Model.Commands.Common;
using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;
using RpgTkoolMvSaveEditor.Model.Queries;
using RpgTkoolMvSaveEditor.Model.Queries.Common;
using RpgTkoolMvSaveEditor.Util.Events;

namespace RpgTkoolMvSaveEditor.Model;

public class ApplicationService(
    Context context,
    SaveDataAutoLoader saveDataAutoLoader,
    CommonSaveDataAutoLoader commonSaveDataAutoLoader,
    IQueryHandler<GetSaveDataQuery, SaveDataViewDto> getSaveDataQueryHandler,
    IQueryHandler<GetCommonSaveDataQuery, CommonSaveDataViewDto> getCommonSaveDataQueryHandler,
    ICommandHandler<UpdateGameSwitchCommand> updateGameSwitchCommandHandler,
    ICommandHandler<UpdateGameVariableCommand> updateGameVariableCommandHandler,
    ICommandHandler<UpdateSwitchCommand> updateSwitchCommandHandler,
    ICommandHandler<UpdateVariableCommand> updateVariableCommandHandler,
    ICommandHandler<UpdateItemCommand> updateItemCommandHandler,
    ICommandHandler<UpdateWeaponCommand> updateWeaponCommandHandler,
    ICommandHandler<UpdateArmorCommand> updateArmorCommandHandler,
    ICommandHandler<UpdateGoldCommand> updateGoldCommandHandler
)
{
    public Event<SaveDataLoadedEventArgs> SaveDataLoaded { get; } = new();
    public Event<CommonSaveDataLoadedEventArgs> CommonSaveDataLoaded { get; } = new();
    public Event<ErrorOccurredEventArgs> ErrorOccurred { get; } = new();

    public async Task SelectWwwDirAsync(string path)
    {
        context.WwwDirPath = path;
        if ((await getSaveDataQueryHandler.HandleAsync(new())).Unwrap(out var saveData, out var message))
        {
            SaveDataLoaded.Publish(new(saveData));
        }
        else
        {
            ErrorOccurred.Publish(new(message));
        }
        saveDataAutoLoader.Start();
        if ((await getCommonSaveDataQueryHandler.HandleAsync(new())).Unwrap(out var commonSaveData, out message))
        {
            CommonSaveDataLoaded.Publish(new(commonSaveData));
        }
        else
        {
            ErrorOccurred.Publish(new(message));
        }
        commonSaveDataAutoLoader.Start();
    }

    public async Task UpdateGameSwitchAsync(UpdateGameSwitchCommand command)
    {
        await updateGameSwitchCommandHandler.HandleAsync(command);
    }

    public async Task UpdateGameVariableAsync(UpdateGameVariableCommand command)
    {
        await updateGameVariableCommandHandler.HandleAsync(command);
    }

    public async Task UpdateSwitchAsync(UpdateSwitchCommand command)
    {
        await updateSwitchCommandHandler.HandleAsync(command);
    }

    public async Task UpdateVariableAsync(UpdateVariableCommand command)
    {
        await updateVariableCommandHandler.HandleAsync(command);
    }

    public async Task UpdateItemAsync(UpdateItemCommand command)
    {
        await updateItemCommandHandler.HandleAsync(command);
    }

    public async Task UpdateWeaponAsync(UpdateWeaponCommand command)
    {
        await updateWeaponCommandHandler.HandleAsync(command);
    }

    public async Task UpdateArmorAsync(UpdateArmorCommand command)
    {
        await updateArmorCommandHandler.HandleAsync(command);
    }

    public async Task UpdateGoldAsync(UpdateGoldCommand command)
    {
        await updateGoldCommandHandler.HandleAsync(command);
    }
}

public class Context(string[]? args = default)
{
    // 引数でファイルパスを渡すとインターフェースの汎用性が下がるのでコンストラクタで渡すようにした
    public string? WwwDirPath { get; set; } = args?.Length > 0 ? args[0] : null;
    // セーブデータの値を変更した直後のロードを抑制する
    public bool SaveDataLoadSuppressed { get; set; }
    // 共通セーブデータの値を変更した直後のロードを抑制する
    public bool CommonSaveDataLoadSuppressed { get; set; }
}
