using System.Threading;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class ExceptionErrorHandler : IRequestHandler<ShowExceptionErrorCommand, Unit>
{
    private readonly IMessageUnit _messageUnit;

    public ExceptionErrorHandler(IMessageUnit messageUnit)
    {
        _messageUnit = messageUnit;
    }
    public Task<Unit> Handle(ShowExceptionErrorCommand request, CancellationToken cancellationToken)
    {
        _messageUnit.ErrorMessage(request.Exception);
        return Unit.Task;
    }
}