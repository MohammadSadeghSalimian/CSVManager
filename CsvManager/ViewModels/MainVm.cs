using System;
using CsvManager.Services;
using MediatR;
using Splat;

namespace CsvManager.ViewModels
{
    public class MainVm:BaseViewModel
    {
       

        public MainVm( IKeyContainer keyContainer,IMediator mediator) : base(keyContainer,mediator)
        {
           
            SetupProperties();
        }

        public MergeVm?  MergeVm { get;private set; }
        protected sealed override void SetupProperties()
        {
            MergeVm = Locator.Current.GetService<MergeVm>() ??
                      throw new ApplicationException("MergeVM must not be null");

        }
    }
}
