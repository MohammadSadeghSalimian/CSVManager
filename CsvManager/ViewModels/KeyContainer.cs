using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CsvManager.ViewModels;

public class KeyContainer: ReactiveObject,IKeyContainer
{
    [Reactive] public bool Started { get; set; }
    [Reactive] public bool Reset { get; set; }
    [Reactive] public bool HasValidAddress { get; set; }
    [Reactive] public bool CameraReady { get; set; }
}