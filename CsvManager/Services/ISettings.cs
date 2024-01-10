using CsvManager.Core.Services;

namespace CsvManager.Services;

public interface ISettings
{
    public IDataSettings Data { get; }
    public void Load(string fileName);
    public void Load();
    public void Save();
    public void SaveAs(string fileName);
    public string GetText();
    public void SetText(string text);

}