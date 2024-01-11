using System;
using CsvManager.Services;
using MediatR;

namespace CsvManager.ViewModels;

public abstract class BaseViewModelWithProgress:BaseViewModel
{
    protected BaseViewModelWithProgress(IKeyContainer keyContainer, IMediator mediator,IMessageUnit messageUnit) : base(keyContainer, mediator,messageUnit)
    {
        
    }
    protected IProgress<double>? Progress;
    public void SetProgress(IProgress<double> progress)
    {
        Progress=progress;
    }
}