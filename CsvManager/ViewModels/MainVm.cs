using System;
using CsvManager.Services;
using MediatR;
using Splat;

namespace CsvManager.ViewModels
{
    public sealed class MainVm:BaseViewModel
    {
       

        public MainVm( IKeyContainer keyContainer,IMediator mediator, IMessageUnit messageUnit,DataCreatorVm dataCreatorVm) : base(keyContainer,mediator,messageUnit)
        {
           
            SetupProperties();
            DataCreatorVm=dataCreatorVm;
        }

        public DataCreatorVm?  DataCreatorVm { get;private set; }
       
    }
}
