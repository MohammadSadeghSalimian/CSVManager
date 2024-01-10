using CsvManager.Services;
using MediatR;
using ReactiveUI;

namespace CsvManager.ViewModels
{
    public abstract class BaseViewModel:ReactiveObject
    {
        
        protected readonly IKeyContainer KeyContainer;
        protected readonly IMediator Mediator;
      
        protected BaseViewModel(IKeyContainer keyContainer,IMediator mediator)
        {
         
            KeyContainer = keyContainer;
            Mediator = mediator;
           
        }

        protected virtual void SetupProperties()
        {

        }

        protected virtual void SetupCommands()
        {

        }
    }
}
