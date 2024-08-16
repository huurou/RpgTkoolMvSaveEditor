using System.Text.Json;
using RpgTkoolMvSaveEditor.Model.Configs;

namespace RpgTkoolMvSaveEditor.Model.Settings;

public class SettingRepository : ISettingRepository
{
    private readonly JsonSerializerOptions options_ = new(JsonSerializerDefaults.Web) { WriteIndented = true };

    public Setting Load()
    {
        if (File.Exists(Paths.SettingsJson))
        {
            var json = File.ReadAllText(Paths.SettingsJson);
            var setting = JsonSerializer.Deserialize<Setting>(json, options_) ?? Setting.Default;
            return setting;
        }
        else
        {
            Save(Setting.Default);
            return Setting.Default;
        }
    }

    public void Save(Setting setting)
    {
        var json = JsonSerializer.Serialize(setting, options_);
        File.WriteAllText(Paths.SettingsJson, json);
    }
}