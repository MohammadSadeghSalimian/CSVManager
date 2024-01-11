using CsvManager.Services;
using MediatR;
using ReactiveUI;

namespace CsvManager.ViewModels
{
    public abstract class BaseViewModel:ReactiveObject
    {
        
        protected readonly IKeyContainer KeyContainer;
        protected readonly IMediator Mediator;
        protected readonly IMessageUnit MessageUnit;
      
        protected BaseViewModel(IKeyContainer keyContainer,IMediator mediator,IMessageUnit messageUnit)
        {
         
            KeyContainer = keyContainer;
            Mediator = mediator;
            MessageUnit = messageUnit;
        }

        protected virtual void SetupProperties()
        {

        }

        protected virtual void SetupCommands()
        {

        }
    }
}
