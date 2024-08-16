namespace RpgTkoolMvSaveEditor.Model.Settings;

public interface ISettingRepository
{
    public Setting Load();

    public void Save(Setting setting);
}