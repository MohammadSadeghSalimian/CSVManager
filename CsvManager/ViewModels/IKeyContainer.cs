namespace CsvManager.ViewModels;

public interface IKeyContainer
{
    public bool Started { get; set; }
    public bool Reset { get; set; }
    bool HasValidAddress { get; set; }
    bool CameraReady { get; set; }
}