using System;
using CsvManager.Services;
using MediatR;
using Splat;

namespace CsvManager.ViewModels
{
    public class MainVm:BaseViewModel
    {
       

        public MainVm( IKeyContainer keyContainer,IMediator mediator, IMessageUnit messageUnit) : base(keyContainer,mediator,messageUnit)
        {
           
            SetupProperties();
        }

        public MergeCsvVm?  MergeVm { get;private set; }
        protected sealed override void SetupProperties()
        {
            MergeVm = Locator.Current.GetService<MergeCsvVm>() ??
                      throw new ApplicationException("MergeVM must not be null");

        }
    }
}
