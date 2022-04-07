namespace RpgTkoolMvSaveEditor.Controls;

internal class SaveDataVM
{
    public string FileName { get; set; }
    public SaveDataControlVM Data { get; set; }

    public SaveDataVM(string fileName, SaveDataControlVM data)
    {
        FileName = fileName;
        Data = data;
    }
}