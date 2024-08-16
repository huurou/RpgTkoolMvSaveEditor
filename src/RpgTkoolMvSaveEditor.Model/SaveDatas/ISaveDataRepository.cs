using RpgTkoolMvSaveEditor.Util.Events;

namespace RpgTkoolMvSaveEditor.Model.SaveDatas;

public interface ISaveDataRepository
{
    public Event<ErrorOccurredEventArgs> ErrorOccurred { get; }

    Task<SaveData?> LoadAsync(string wwwDirPath);

    Task SaveAsync(SaveData saveData);
}

public record SaveDataRepository : ISaveDataRepository
{
    public Event<ErrorOccurredEventArgs> ErrorOccurred { get; } = new();

    public async Task<SaveData?> LoadAsync(string wwwDirPath)
    {
        if (!Directory.Exists(wwwDirPath))
        {
            ErrorOccurred.Publish(new($"{wwwDirPath}は存在しないかフォルダではありません。"));
            return null;
        }
        if (Path.GetFileName(wwwDirPath) != "www")
        {
            ErrorOccurred.Publish(new($"wwwフォルダを指定して下さい"));
            return null;
        }
        // wwwフォルダを読み込む人がwwwフォルダかどうかや各ファイルのパスを管理する
        // 各ファイルは保存の必要がないものはまとめてローダーで読み出す
        // 読み出したゲームデータをセーブデータとコモンセーブデータのリポジトリに渡す
        // dataフォルダにあるゲームデータをロードする
        // save/file1.rpgsave save/common.rpgsave をjsonにして読み込む 読み込みながらゲームデータと突き合わせてSaveDataオブジェクトを作成する

        throw new NotImplementedException();
    }

    public Task SaveAsync(SaveData saveData)
    {
        throw new NotImplementedException();
    }
}
