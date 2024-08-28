using RpgTkoolMvSaveEditor.Model.CommonSaveDatas;
using RpgTkoolMvSaveEditor.Model.GameData.SaveDatas;

namespace RpgTkoolMvSaveEditor.Model;

public record SaveDataLoadedEventArgs(SaveDataViewDto SaveData);
public record CommonSaveDataLoadedEventArgs(CommonSaveDataViewDto CommonSaveData);
public record ErrorOccurredEventArgs(string Messaga);
