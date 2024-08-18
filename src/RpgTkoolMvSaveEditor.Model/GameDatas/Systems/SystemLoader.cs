﻿using RpgTkoolMvSaveEditor.Util.Results;
using System.Text.Json;

namespace RpgTkoolMvSaveEditor.Model.GameDatas.Systems;

public class SystemLoader(WwwContext wwwContext) : ISystemLoader
{
    private readonly JsonSerializerOptions options_ = new(JsonSerializerDefaults.Web);
    private System? data_;

    public async Task<Result<System>> LoadAsync()
    {
        if (data_ is not null) { return new Ok<System>(data_); }
        var filePath = Path.Combine(wwwContext.WwwDirPath, "data", "System.json");
        if (!File.Exists(filePath)) { return new Err<System>($"{filePath}が存在しません。"); }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        var dto = await JsonSerializer.DeserializeAsync<SystemDataDto>(fileStream, options_);
        if (dto is not null)
        {
            data_ = dto.ToModel();
            return new Ok<System>(data_);
        }
        else
        {
            return new Err<System>();
        }
    }
}
