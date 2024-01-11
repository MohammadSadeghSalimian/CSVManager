using System;
using CsvManager.Services;
using MediatR;
using ReactiveUI.Fody.Helpers;

namespace CsvManager.ViewModels;

public sealed class DataCreatorVm:BaseViewModel
{
    public DataCreatorVm(IKeyContainer keyContainer, IMediator mediator, SqLiteDatabaseVm sqLiteDatabaseVm, MergeCsvVm mergeVm, IMessageUnit messageUnit) : base(keyContainer, mediator,messageUnit)
    {
        SqLiteDatabaseVm = sqLiteDatabaseVm;
        MergeVm = mergeVm;
        var progress = new Progress<double>(x =>
        {
            this.Progress = x;
        });
        SqLiteDatabaseVm.SetProgress(progress);
        MergeVm.SetProgress(progress);
        SetupCommands();
    }
    public SqLiteDatabaseVm SqLiteDatabaseVm { get; private set; }
    public MergeCsvVm MergeVm { get; private set; }
    [Reactive] public double Progress { get; private set; }
}